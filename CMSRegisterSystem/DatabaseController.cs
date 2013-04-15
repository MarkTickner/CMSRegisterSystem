using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace CMSRegisterSystem
{
    public sealed class DatabaseController
    {
        // Singleton pattern start
        static DatabaseController instance = null;
        static readonly object padlock = new object();

        DatabaseController()
        {
        }

        public static DatabaseController Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DatabaseController();
                    }
                    return instance;
                }
            }
        }
        // Singleton pattern end

        public static OleDbConnection GetConnection()
        {
            string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\MainDatabase.accdb"; // Database path, |DataDirectory| points to the App_Data folder
            //string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\databases\tm112\CMSRegisterSystem\MainDatabase.accdb";

            return new OleDbConnection(connString);
        }

        // SHARED ---------------------------------------------------------------------------------------------------
        // Method that checks user log in details and returns 'logInState'
        public static void LogInUser(string username, string password)
        {
            Student student;
            Staff staff;

            // SQL query to send to database
            string queryStudent = "SELECT * FROM Student WHERE StudentUserID = '" + username + "' AND StudentPassword = '" + password + "'";
            string queryStaff = "SELECT * FROM Staff WHERE StaffUserID = '" + username + "' AND StaffPassword = '" + password + "'";

            OleDbConnection myConnection = GetConnection();

            OleDbCommand commandStudent = new OleDbCommand(queryStudent, myConnection);
            OleDbCommand commandStaff = new OleDbCommand(queryStaff, myConnection);

            List<StaffType> staffTypes = ListStaffTypes();

            try
            {
                myConnection.Open();

                OleDbDataReader readerStudent = commandStudent.ExecuteReader();
                OleDbDataReader readerStaff = commandStaff.ExecuteReader();

                if (readerStudent.HasRows == true)
                {
                    // User is found in 'Student' table
                    readerStudent.Read();

                    student = new Student(int.Parse(readerStudent["StudentID"].ToString()), readerStudent["StudentUserID"].ToString(), readerStudent["StudentFirstName"].ToString(), readerStudent["StudentSurname"].ToString(), readerStudent["StudentRegistrationStatus"].ToString(), readerStudent["StudentHolds"].ToString(), readerStudent["StudentPhoto"].ToString(), int.Parse(readerStudent["StudentYearOfStudy"].ToString()), readerStudent["ProgrammeID"].ToString());
                    HttpContext.Current.Session["loggedInStudent"] = student;

                    commandStudent.ExecuteNonQuery();
                }
                else if (readerStaff.HasRows == true)
                {
                    // User is found in 'Staff' table
                    readerStaff.Read();

                    StaffType staffType = FindStaffType(staffTypes, int.Parse(readerStaff["StaffType"].ToString()));

                    staff = new Staff(int.Parse(readerStaff["StaffID"].ToString()), readerStaff["StaffUserID"].ToString(), readerStaff["StaffTitle"].ToString(), readerStaff["StaffFirstName"].ToString(), readerStaff["StaffSurname"].ToString(), staffType);
                    HttpContext.Current.Session["loggedInStaff"] = staff;

                    commandStaff.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a list of 'StaffType' objects
        public static List<StaffType> ListStaffTypes()
        {
            List<StaffType> staffTypes = new List<StaffType>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM StaffType";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    staffTypes.Add(new StaffType(int.Parse(myReader["StaffTypeID"].ToString()), myReader["StaffType"].ToString()));
                }
                return staffTypes;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a 'StaffType' object by 'StaffTypeID'
        private static StaffType FindStaffType(List<StaffType> staffTypes, int id)
        {
            foreach (var staffType in staffTypes)
            {
                if (staffType.StaffTypeID == id)
                {
                    return staffType;
                }
            }
            return null;
        }
        // SHARED ---------------------------------------------------------------------------------------------------

        // MARK -----------------------------------------------------------------------------------------------------
        // Method that returns a list of 'Attendance' objects
        public static List<Attendance> ListAttendances()
        {
            List<Attendance> attendances = new List<Attendance>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM Attendance";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<TeachingSession> teachingSessions = ListTeachingSessions();
            List<Student> students = ListStudents();

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    TeachingSession currentTeachingSession = FindTeachingSession(teachingSessions, int.Parse(myReader["SessionID"].ToString()));
                    Student currentStudent = FindStudent(students, int.Parse(myReader["StudentID"].ToString()));
                    attendances.Add(new Attendance(int.Parse(myReader["AttendanceID"].ToString()), currentTeachingSession, currentStudent, Convert.ToBoolean(myReader["Present"].ToString())));
                }
                return attendances;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a list of 'Attendance' objects (absent only)
        public static List<Attendance> ListAttendancesAbsentOnly()
        {
            List<Attendance> attendances = new List<Attendance>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM Attendance WHERE Present = false";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<TeachingSession> teachingSessions = ListTeachingSessions();
            List<Student> students = ListStudents();

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    TeachingSession currentTeachingSession = FindTeachingSession(teachingSessions, int.Parse(myReader["SessionID"].ToString()));
                    Student currentStudent = FindStudent(students, int.Parse(myReader["StudentID"].ToString()));
                    attendances.Add(new Attendance(int.Parse(myReader["AttendanceID"].ToString()), currentTeachingSession, currentStudent, Convert.ToBoolean(myReader["Present"].ToString())));
                }
                return attendances;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a list of 'Attendance' objects (absent only) by 'StudentID'
        public static List<Attendance> ListAttendancesAbsentOnlyByStudentID(int studentID)
        {
            List<Attendance> attendances = new List<Attendance>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM Attendance WHERE Present = false AND StudentID = " + studentID;
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<TeachingSession> teachingSessions = ListTeachingSessions();
            List<Student> students = ListStudents();

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    TeachingSession currentTeachingSession = FindTeachingSession(teachingSessions, int.Parse(myReader["SessionID"].ToString()));
                    Student currentStudent = FindStudent(students, int.Parse(myReader["StudentID"].ToString()));
                    attendances.Add(new Attendance(int.Parse(myReader["AttendanceID"].ToString()), currentTeachingSession, currentStudent, Convert.ToBoolean(myReader["Present"].ToString())));
                }
                return attendances;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a list of 'Attendance' objects (absent and notified only)
        public static List<Attendance> ListAttendancesAbsentAndNotifiedOnly()
        {
            List<Attendance> attendances = new List<Attendance>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM Attendance WHERE Present = false";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<TeachingSession> teachingSessions = ListTeachingSessions();
            List<Student> students = ListStudents();
            List<NotifiedAbsence> notifiedAbsences = ListNotifiedAbsences();

            int attendanceID;

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    attendanceID = int.Parse(myReader["AttendanceID"].ToString());

                    if (CheckIfAbsenceIsAlreadyNotified(attendanceID, notifiedAbsences))
                    {
                        TeachingSession currentTeachingSession = FindTeachingSession(teachingSessions, int.Parse(myReader["SessionID"].ToString()));
                        Student currentStudent = FindStudent(students, int.Parse(myReader["StudentID"].ToString()));
                        attendances.Add(new Attendance(attendanceID, currentTeachingSession, currentStudent, Convert.ToBoolean(myReader["Present"].ToString())));
                    }
                }
                return attendances;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a list of 'Attendance' objects by 'AttendanceID'
        public static List<Attendance> ListAttendancesByAttendanceID(int attendanceID)
        {
            List<Attendance> attendances = new List<Attendance>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM Attendance WHERE AttendanceID = " + attendanceID;
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<TeachingSession> teachingSessions = ListTeachingSessions();
            List<Student> students = ListStudents();

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    TeachingSession currentTeachingSession = FindTeachingSession(teachingSessions, int.Parse(myReader["SessionID"].ToString()));
                    Student currentStudent = FindStudent(students, int.Parse(myReader["StudentID"].ToString()));
                    attendances.Add(new Attendance(int.Parse(myReader["AttendanceID"].ToString()), currentTeachingSession, currentStudent, Convert.ToBoolean(myReader["Present"].ToString())));
                }
                return attendances;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a list of 'Attendance' objects by 'StudentID'
        public static List<Attendance> ListAttendancesByStudentID(int studentID)
        {
            List<Attendance> attendances = new List<Attendance>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM Attendance WHERE StudentID = " + studentID;
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<TeachingSession> teachingSessions = ListTeachingSessions();
            List<Student> students = ListStudents();

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    TeachingSession currentTeachingSession = FindTeachingSession(teachingSessions, int.Parse(myReader["SessionID"].ToString()));
                    Student currentStudent = FindStudent(students, int.Parse(myReader["StudentID"].ToString()));
                    attendances.Add(new Attendance(int.Parse(myReader["AttendanceID"].ToString()), currentTeachingSession, currentStudent, Convert.ToBoolean(myReader["Present"].ToString())));
                }
                return attendances;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns an 'Attendance' object by 'AttendanceID'
        private static Attendance FindAttendance(List<Attendance> attendances, int id)
        {
            foreach (var attendance in attendances)
            {
                if (attendance.AttendanceID == id)
                {
                    return attendance;
                }
            }
            return null;
        }

        // Method that returns a list of 'NotifiedAbsenceAuthorised' objects
        public static List<NotifiedAbsenceAuthorised> ListNotifiedAbsenceAuthoriseds()
        {
            List<NotifiedAbsenceAuthorised> notifiedAbsenceAuthoriseds = new List<NotifiedAbsenceAuthorised>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM NotifiedAbsenceAuthorised";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<NotifiedAbsence> notifiedAbsences = ListNotifiedAbsences();

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    NotifiedAbsence currentNotifiedAbsence = FindNotifiedAbsence(notifiedAbsences, int.Parse(myReader["AbsenceID"].ToString()));
                    notifiedAbsenceAuthoriseds.Add(new NotifiedAbsenceAuthorised(int.Parse(myReader["AuthorisedAbsenceID"].ToString()), currentNotifiedAbsence, Convert.ToBoolean(myReader["Authorised"].ToString()), myReader["AuthorisedRejectedDate"].ToString()));
                }
                return notifiedAbsenceAuthoriseds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a list of 'NotifiedAbsenceAuthorised' objects by 'AbsenceID'
        public static List<NotifiedAbsenceAuthorised> ListNotifiedAbsenceAuthorisedsByAbsenceID(int absenceID)
        {
            List<NotifiedAbsenceAuthorised> notifiedAbsenceAuthoriseds = new List<NotifiedAbsenceAuthorised>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM NotifiedAbsenceAuthorised WHERE AbsenceID = " + absenceID;
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<NotifiedAbsence> notifiedAbsences = ListNotifiedAbsencesByAbsenceID(absenceID);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    NotifiedAbsence currentNotifiedAbsence = FindNotifiedAbsence(notifiedAbsences, int.Parse(myReader["AbsenceID"].ToString()));
                    notifiedAbsenceAuthoriseds.Add(new NotifiedAbsenceAuthorised(int.Parse(myReader["AuthorisedAbsenceID"].ToString()), currentNotifiedAbsence, Convert.ToBoolean(myReader["Authorised"].ToString()), myReader["AuthorisedRejectedDate"].ToString()));
                }
                return notifiedAbsenceAuthoriseds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns an 'NotifiedAbsenceAuthorised' object by 'AuthorisedAbsenceID'
        private static NotifiedAbsenceAuthorised FindNotifiedAbsenceAuthorised(List<NotifiedAbsenceAuthorised> notifiedAbsenceAuthoriseds, int id)
        {
            foreach (var notifiedAbsenceAuthorised in notifiedAbsenceAuthoriseds)
            {
                if (notifiedAbsenceAuthorised.AuthorisedAbsenceID == id)
                {
                    return notifiedAbsenceAuthorised;
                }
            }
            return null;
        }

        // Method that returns a list of 'TeachingSession' objects
        public static List<TeachingSession> ListTeachingSessions()
        {
            List<TeachingSession> teachingSessions = new List<TeachingSession>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM TeachingSession";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<Course> courses = ListCourses();
            List<Room> rooms = ListRooms();
            List<Staff> staffs = ListStaffs();

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    Course currentCourse = FindCourse(courses, myReader["CourseCode"].ToString());
                    Room currentRoom = FindRoom(rooms, myReader["RoomID"].ToString());
                    Staff currentStaff = FindStaff(staffs, int.Parse(myReader["LeadStaffID"].ToString()));
                    teachingSessions.Add(new TeachingSession(int.Parse(myReader["SessionID"].ToString()), myReader["SessionType"].ToString(), currentCourse, currentRoom, myReader["SessionDate"].ToString(), myReader["Day"].ToString(), myReader["Time"].ToString(), int.Parse(myReader["Duration"].ToString()), currentStaff));
                }
                return teachingSessions;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a 'TeachingSession' object by 'SessionID'
        private static TeachingSession FindTeachingSession(List<TeachingSession> teachingSessions, int id)
        {
            foreach (var teachingSession in teachingSessions)
            {
                if (teachingSession.SessionID == id)
                {
                    return teachingSession;
                }
            }
            return null;
        }

        // Method that returns a list of 'Student' objects
        public static List<Student> ListStudents()
        {
            List<Student> students = new List<Student>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM Student";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    students.Add(new Student(int.Parse(myReader["StudentID"].ToString()), myReader["StudentUserID"].ToString(), myReader["StudentFirstName"].ToString(), myReader["StudentSurname"].ToString(), myReader["StudentRegistrationStatus"].ToString(), myReader["StudentHolds"].ToString(), myReader["StudentPhoto"].ToString(), int.Parse(myReader["StudentYearOfStudy"].ToString()), myReader["ProgrammeID"].ToString()));
                }
                return students;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a 'Student' object by 'StudentID'
        private static Student FindStudent(List<Student> students, int id)
        {
            foreach (var student in students)
            {
                if (student.StudentID == id)
                {
                    return student;
                }
            }
            return null;
        }

        // Method that returns a list of 'Course' objects
        public static List<Course> ListCourses()
        {
            List<Course> courses = new List<Course>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM Course";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<Staff> staffs = ListStaffs();

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    Staff currentStaff = FindStaff(staffs, int.Parse(myReader["CourseCoordinator"].ToString()));
                    courses.Add(new Course(myReader["CourseCode"].ToString(), myReader["CourseName"].ToString(), int.Parse(myReader["CourseLength"].ToString()), currentStaff));
                }
                return courses;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a 'Course' object by 'CourseCode'
        private static Course FindCourse(List<Course> courses, string id)
        {
            foreach (var course in courses)
            {
                if (course.CourseCode == id)
                {
                    return course;
                }
            }
            return null;
        }

        // Method that returns a list of 'Staff' objects
        public static List<Staff> ListStaffs()
        {
            List<Staff> staffs = new List<Staff>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM Staff";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<StaffType> staffTypes = ListStaffTypes();

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    StaffType currentStaffType = FindStaffType(staffTypes, int.Parse(myReader["StaffType"].ToString()));
                    staffs.Add(new Staff(int.Parse(myReader["StaffID"].ToString()), myReader["StaffUserID"].ToString(), myReader["StaffTitle"].ToString(), myReader["StaffFirstName"].ToString(), myReader["StaffSurname"].ToString(), currentStaffType));
                }
                return staffs;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a 'Staff' object by 'StaffID'
        private static Staff FindStaff(List<Staff> staffs, int id)
        {
            foreach (var staff in staffs)
            {
                if (staff.StaffID == id)
                {
                    return staff;
                }
            }
            return null;
        }

        // Method that returns a list of 'Room' objects
        public static List<Room> ListRooms()
        {
            List<Room> rooms = new List<Room>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM Room";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    rooms.Add(new Room(myReader["RoomID"].ToString(), myReader["RoomName"].ToString()));
                }
                return rooms;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a 'Room' object by 'RoomID'
        private static Room FindRoom(List<Room> rooms, string id)
        {
            foreach (var room in rooms)
            {
                if (room.RoomID == id)
                {
                    return room;
                }
            }
            return null;
        }

        // Method that returns a list of 'NotifiedAbsence' objects
        public static List<NotifiedAbsence> ListNotifiedAbsences()
        {
            List<NotifiedAbsence> notifiedAbsences = new List<NotifiedAbsence>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM NotifiedAbsence";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<Attendance> attendances = ListAttendances();

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    Attendance currentAttendance = FindAttendance(attendances, int.Parse(myReader["AttendanceID"].ToString()));
                    notifiedAbsences.Add(new NotifiedAbsence(int.Parse(myReader["AbsenceID"].ToString()), currentAttendance, myReader["Reason"].ToString(), myReader["NotifiedDate"].ToString()));
                }
                return notifiedAbsences;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a list of 'NotifiedAbsence' objects by 'AbsenceID'
        public static List<NotifiedAbsence> ListNotifiedAbsencesByAbsenceID(int absenceID)
        {
            List<NotifiedAbsence> notifiedAbsences = new List<NotifiedAbsence>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM NotifiedAbsence WHERE AbsenceID = " + absenceID;
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<Attendance> attendances = ListAttendancesByAttendanceID(absenceID);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    Attendance currentAttendance = FindAttendance(attendances, int.Parse(myReader["AttendanceID"].ToString()));
                    notifiedAbsences.Add(new NotifiedAbsence(int.Parse(myReader["AbsenceID"].ToString()), currentAttendance, myReader["Reason"].ToString(), myReader["NotifiedDate"].ToString()));
                }
                return notifiedAbsences;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a list of 'NotifiedAbsence' objects by 'AttendanceID'
        public static List<NotifiedAbsence> ListNotifiedAbsencesByAttendanceID(int attendanceID)
        {
            List<NotifiedAbsence> notifiedAbsences = new List<NotifiedAbsence>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM NotifiedAbsence WHERE AttendanceID = " + attendanceID;
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            List<Attendance> attendances = ListAttendancesByAttendanceID(attendanceID);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    Attendance currentAttendance = FindAttendance(attendances, int.Parse(myReader["AttendanceID"].ToString()));
                    notifiedAbsences.Add(new NotifiedAbsence(int.Parse(myReader["AbsenceID"].ToString()), currentAttendance, myReader["Reason"].ToString(), myReader["NotifiedDate"].ToString()));
                }
                return notifiedAbsences;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a 'NotifiedAbsence' object by 'AbsenceID'
        private static NotifiedAbsence FindNotifiedAbsence(List<NotifiedAbsence> notifiedAbsences, int id)
        {
            foreach (var notifiedAbsence in notifiedAbsences)
            {
                if (notifiedAbsence.AbsenceID == id)
                {
                    return notifiedAbsence;
                }
            }
            return null;
        }

        // Method which checks to see if an absence has already been notified, using the 'AttendanceID'
        public static bool CheckIfAbsenceIsAlreadyNotified(int attendanceID)
        {
            List<NotifiedAbsence> notifiedAbsences = ListNotifiedAbsencesByAttendanceID(attendanceID);

            // Check to see if the 'notifiedAbsences' list is empty
            if (notifiedAbsences.Count() != 0)
            {
                // Absence found
                return true;
            }
            else
            {
                // Absence not found
                return false;
            }
        }

        // Method which checks to see if an absence has already been notified, using the 'AttendanceID' and a list of 'NotifiedAbsence' objects
        public static bool CheckIfAbsenceIsAlreadyNotified(int attendanceID, List<NotifiedAbsence> notifiedAbsences)
        {
            bool notified = false;

            foreach (var notifiedAbsence in notifiedAbsences)
            {
                if (notifiedAbsence.Attendance.AttendanceID == attendanceID)
                {
                    // Absence found
                    notified = true;
                }
            }

            return notified;
        }

        // Method that saves an absence notification to the database
        public static bool SaveNotification(int attendanceID, string reason)
        {
            OleDbConnection myConnection = GetConnection();

            string myQuery = "INSERT INTO NotifiedAbsence (AttendanceID, Reason, NotifiedDate) VALUES (" + attendanceID + ", '" + reason + "', '" + DateTime.Now.ToShortDateString() + "')";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                return false;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that saves an absence notification authorisation or rejection to the database
        public static bool SaveNotificationAuthorisationRejection(int absenceID, bool authorised)
        {
            OleDbConnection myConnection = GetConnection();

            string myQuery = "INSERT INTO NotifiedAbsenceAuthorised (AbsenceID, Authorised, AuthorisedRejectedDate) VALUES (" + absenceID + ", " + authorised + ", '" + DateTime.Now.ToShortDateString() + "')";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);

                return false;
            }
            finally
            {
                myConnection.Close();
            }
        }
        // MARK -----------------------------------------------------------------------------------------------------

        // HOLLY ----------------------------------------------------------------------------------------------------
        // Method to return Teaching Sessions
        public static List<TeachingSession> ListTeachingSession(int StaffID)
        {
            List<TeachingSession> TeachingSession = new List<TeachingSession>();
            OleDbConnection myConnection = GetConnection();
            string myQuery = "SELECT * FROM TeachingSession WHERE LeadStaffID = " + StaffID + "";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    Course currentCourse = getCurrentCourse(myReader["CourseCode"].ToString());
                    Staff currentStaff = getCurrentStaff(myReader["LeadStaffID"].ToString());
                    Room currentRoom = getCurrentRoom(myReader["RoomID"].ToString());

                    TeachingSession.Add(new TeachingSession(int.Parse(myReader["sessionID"].ToString()), myReader["sessionType"].ToString(), currentCourse, currentRoom, myReader["SessionDate"].ToString(), myReader["Day"].ToString(), myReader["Time"].ToString(), int.Parse(myReader["Duration"].ToString()), currentStaff));
                }
                return TeachingSession;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method to return Course Object
        public static Course getCurrentCourse(String CourseCode)
        {
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM Course WHERE CourseCode = '" + CourseCode + "'";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                Course currentCourse = null;
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    currentCourse = new Course(myReader["CourseCode"].ToString(), myReader["CourseName"].ToString(), int.Parse(myReader["CourseLength"].ToString()), getCurrentStaff(myReader["CourseCoordinator"].ToString()));
                }
                return currentCourse;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method to return Staff Object
        public static Staff getCurrentStaff(String StaffID)
        {
            OleDbConnection myConnection = GetConnection();
            int sID = int.Parse(StaffID);
            string myQuery = "SELECT StaffID, StaffUserID, StaffTitle, StaffFirstName, StaffSurname, StaffType FROM Staff WHERE StaffID = " + sID + "";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                Staff currentStaff = null;
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    string currentstafftype = myReader["StaffType"].ToString();
                    currentStaff = new Staff(int.Parse(myReader["StaffID"].ToString()), myReader["StaffUserID"].ToString(), myReader["StaffTitle"].ToString(), myReader["StaffFirstName"].ToString(), myReader["StaffSurname"].ToString(), getStaffType(currentstafftype));
                }
                return currentStaff;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method to return StaffType Object
        public static StaffType getStaffType(String StaffType)
        {
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT StaffTypeID, StaffType FROM StaffType WHERE StaffType = '" + StaffType + "'";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                StaffType currentStaffType = null;
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    currentStaffType = new StaffType(int.Parse(myReader["StaffTypeID"].ToString()), myReader["StaffTypeUserID"].ToString());
                }
                return currentStaffType;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method to return Room Object
        public static Room getCurrentRoom(String RoomID)
        {
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT RoomID, RoomName FROM Room WHERE RoomID = '" + RoomID + "'";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                Room currentRoom = null;
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    currentRoom = new Room(myReader["RoomID"].ToString(), myReader["RoomName"].ToString());
                }
                return currentRoom;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method to return teaching session register
        public static List<Student> getRegister(int sessionID)
        {
            List<Student> register = new List<Student>();

            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM Register WHERE SessionID = " + sessionID + "";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {

                    Student student = getStudent(int.Parse(myReader["StudentID"].ToString()));
                    register.Add(student);
                }
                return register;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method to return Student Object
        public static Student getStudent(int StudentID)
        {
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT StudentID, StudentUserID, StudentFirstName, StudentSurname, StudentRegistrationStatus, StudentHolds, StudentPhoto, StudentYearOFStudy, ProgrammeID FROM Student WHERE StudentID = " + StudentID + "";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                Student student = null;
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    student = new Student(int.Parse(myReader["StudentID"].ToString()), myReader["StudentUserID"].ToString(), myReader["StudentFirstName"].ToString(), myReader["StudentSurname"].ToString(), myReader["StudentRegistrationStatus"].ToString(), myReader["StudentHolds"].ToString(), myReader["StudentPhoto"].ToString(), int.Parse(myReader["StudentYearOFStudy"].ToString()), myReader["ProgrammeID"].ToString());
                }
                return student;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method to save register to the database
        public static void saveRegister(List<string> register)
        {
            OleDbConnection myConnection = GetConnection();
            myConnection.Open();
            try
            {
                foreach (string i in register)
                {
                    string str = i;
                    string[] delimiters = new string[] { " " };
                    string[] split = str.Split(delimiters, StringSplitOptions.None);
                    int studentID = int.Parse(split[0]);
                    string presence = split[1];
                    int sessionID = int.Parse(HttpContext.Current.Session["sessionID"].ToString());
                    string date = DateTime.Now.ToShortDateString();
                    string str2 = " ";

                    if (presence == "Yes")
                    {
                        string myQuery = "INSERT INTO [Attendance](SessionID, StudentID, Present, notes, SessionDate) VALUES (" + sessionID + ", " + studentID + ", True, '" + str2 + "', '" + date + "')";
                        OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);
                        myCommand.ExecuteNonQuery();
                    }

                    else if (presence == "No")
                    {
                        string myQuery = "INSERT INTO [Attendance](SessionID, StudentID, Present, notes, SessionDate) VALUES (" + sessionID + ", " + studentID + ", False, '" + str2 + "', '" + date + "')";
                        OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);
                        myCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method to return teaching session dates
        public static List<string> getDates(int sessionID)
        {
            List<string> dates = new List<string>();

            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT DISTINCT SessionDate FROM Attendance WHERE SessionID = " + sessionID + "";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    dates.Add(myReader["SessionDate"].ToString());
                }
                return dates;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method to return Student Objects for editable register
        public static List<string> getRegisterEdit(string sessionID, string sessDate)
        {
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT StudentID, Present FROM Attendance WHERE SessionID = " + sessionID + " AND SessionDate = '" + sessDate + "'";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                List<string> registerEdit = new List<string>();
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();

                while (myReader.Read())
                {
                    Student stu = getStudent(int.Parse(myReader["StudentID"].ToString()));
                    registerEdit.Add(myReader["Present"].ToString() + " " + stu.StudentID + " " + stu.StudentFirstName + " " + stu.StudentSurname + " " + stu.StudentRegistrationStatus);
                }
                return registerEdit;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method to save register to the database
        public static void updateRegister(List<string> studentIDs, string sessionDate)
        {
            OleDbConnection myConnection = GetConnection();
            myConnection.Open();
            try
            {
                foreach (string i in studentIDs)
                {
                    string str = i;
                    string[] delimiters = new string[] { " " };
                    string[] split = str.Split(delimiters, StringSplitOptions.None);
                    int studentID = int.Parse(split[0]);
                    string presence = split[1];
                    int sessionID = (int.Parse(HttpContext.Current.Session["sessionID2"].ToString()));

                    if (presence == "Yes")
                    {
                        string myQuery = "UPDATE Attendance SET Present = True WHERE SessionID = " + sessionID + " AND StudentID = " + studentID + " AND SessionDate = '" + sessionDate + "'";
                        OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);
                        myCommand.ExecuteNonQuery();
                    }

                    else if (presence == "No")
                    {
                        string myQuery = "UPDATE Attendance SET Present = False WHERE SessionID = " + sessionID + " AND StudentID = " + studentID + " AND SessionDate = '" + sessionDate + "'";
                        OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);
                        myCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
            }
            finally
            {
                myConnection.Close();
            }
        }
        // HOLLY ----------------------------------------------------------------------------------------------------

        // FLAVIA ---------------------------------------------------------------------------------------------------
        //Method that store in the database, the information about a member of the staff who will be associated with a teaching session 

        public static bool AssociateStaff(string cn, string cc, string sn, int st, string ss, string rn)
        {
            string queryAssociateStaff = "INSERT INTO StaffCourse VALUES ('" + cn + "', '" + cc + "' , '" + sn + "' , '" + st + "' , '" + ss + "' , '" + rn + "')";

            OleDbConnection myConnection = GetConnection();

            OleDbCommand commandAS = new OleDbCommand(queryAssociateStaff, myConnection);

            try
            {
                myConnection.Open();
                commandAS.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return false;
            }
            finally
            {
                myConnection.Close();
            }
        }

        //Method that store in the database, the information about a student that will be associated with a teaching session
        //(the student ID, the course name, the course code, the session name and the session type)
        public static bool MaintainList(int sID, string cN, string cC, string sN)
        {
            string queryAssociateStudent = "INSERT INTO StudentCourse VALUES ('" + sID + "', '" + cN + "' , '" + cC + "', '" + sN + "')";

            OleDbConnection myConnection = GetConnection();

            OleDbCommand commandAS = new OleDbCommand(queryAssociateStudent, myConnection);

            try
            {
                myConnection.Open();
                commandAS.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return false;
            }
            finally
            {
                myConnection.Close();
            }
        }

        //Method that returns a lists of "TeachingSession" objects (the type of the teaching session)
        public static List<TeachingSession> OccurrenceList()
        {
            List<TeachingSession> occurrenceName = new List<TeachingSession>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT DISTINCT SessionType FROM TeachingSession";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {

                    occurrenceName.Add(new TeachingSession(myReader["SessionType"].ToString()));
                }
                return occurrenceName;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        //Method that returns a lists of "Staff" objects (the staff first name and the staff surname)
        public static List<Staff> StaffList()
        {
            List<Staff> staffName = new List<Staff>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM  Staff";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    staffName.Add(new Staff((int)myReader["staffID"], myReader["StaffFirstName"].ToString(), myReader["StaffSurname"].ToString()));
                }
                return staffName;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        //Method that returns a lists of "Room" objects (the room name)
        public static List<Room> RoomList()
        {
            List<Room> roomNr = new List<Room>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM  Room";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    roomNr.Add(new Room(myReader["RoomID"].ToString(), myReader["RoomName"].ToString()));
                }
                return roomNr;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }

        //Method that returns a lists of "Course" objects (the course name and the course code)
        public static List<Course> ListCoursenames()
        {
            List<Course> courseName = new List<Course>();
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM  Course";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    courseName.Add(new Course(myReader["courseCode"].ToString() + " - " + myReader["courseName"].ToString(), myReader["courseCode"].ToString()));
                }
                return courseName;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }
        }
        // FLAVIA ---------------------------------------------------------------------------------------------------

        // EMILY ----------------------------------------------------------------------------------------------------
        // Method that returns a list of 'StudentAbsent' objects
        public static int ListAbsentStudentsByID(int studentID)
        {
            int count = 0;
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM Attendance WHERE StudentID = " + studentID + " AND PRESENT = FALSE";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    count++;
                }
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return count;
            }
            finally
            {
                myConnection.Close();
            }
        }

        // Method that returns a list of 'StudentPresent' objects
        public static int ListPresentStudentsByID(int studentID)
        {
            int count = 0;
            OleDbConnection myConnection = GetConnection();

            string myQuery = "SELECT * FROM Attendance WHERE StudentID = " + studentID + " AND PRESENT = True";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    count++;
                }
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DBHandler", ex);
                return count;
            }
            finally
            {
                myConnection.Close();
            }
        }

        //method to add unregistered student
        public static string AddUnregisteredStudent(string StudentFirstName, string Surname, string StudentID, string YearofStudy, string ProgrammeID, string CourseCode)
        {
            OleDbConnection myConnection = GetConnection();

            string myQuery = "INSERT INTO Student( [StudentFirstName], [StudentSurname], [StudentID], [StudentYearOFStudy], [ProgrammeID], [StudentRegistrationStatus]) VALUES ('" + StudentFirstName + "' , '" + Surname + "' ,'" + StudentID + "' ,'" + YearofStudy + "' , '" + ProgrammeID + "' , '" + "UnRegistered" + "')";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);

            try
            {
                myConnection.Open();
                myCommand.ExecuteNonQuery();
                return "Student has been adeed to Course";
            }
            catch (Exception ex)
            {
                return "Exception in DBHandler" + ex;
            }
            finally
            {
                myConnection.Close();

            }
            OleDbConnection myConnection2 = GetConnection();

            string myQuery2 = "INSERT INTO StudentCourse([CourseCode], [StudentID]) VALUES ('" + CourseCode + "','" + StudentID + "')";
            OleDbCommand myCommand2 = new OleDbCommand(myQuery2, myConnection2);

            try
            {
                myConnection2.Open();
                myCommand2.ExecuteNonQuery();
                return "Student has been adeed to Course";
            }
            catch (Exception ex)
            {
                return "Exception in DBHandler" + ex;
            }
            finally
            {
                myConnection2.Close();

            }
        }

        //delete student course
        public static string DeleteStudentCourse(int StudentID, string CourseCode)
        {
            OleDbConnection myConnection2 = GetConnection();
            string myQuery2 = "DELETE * FROM StudentCourse WHERE StudentID = " + (StudentID + " AND CourseCode = '" + CourseCode + "'");
            OleDbCommand myCommand2 = new OleDbCommand(myQuery2, myConnection2);

            try
            {
                myConnection2.Open();
                myCommand2.ExecuteNonQuery();
                return "Sussesfully Deleted";
            }
            catch (Exception ex)
            {
                return "Student not Deleted, Please Check details and try again " + ex;
            }
            finally
            {
                myConnection2.Close();
            }
        }

        //delete student
        public static string DeleteStudent(int StudentID)
        {
            OleDbConnection myConnection2 = GetConnection();
            string myQuery = "DELETE * FROM Student WHERE StudentID = " + StudentID + "";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection2);

            try
            {
                myConnection2.Open();
                myCommand.ExecuteNonQuery();
                return "Sussesfully Deleted";
            }
            catch (Exception ex)
            {
                return "Student not Deleted, Please Check details and try again " + ex;
            }
            finally
            {
                myConnection2.Close();
            }
        }

        //delete course
        public static string DeleteCourse(string CourseCode)
        {
            OleDbConnection myConnection2 = GetConnection();
            string myQuery4 = "DELETE * FROM Course WHERE CourseCode = '" + CourseCode + "'";
            OleDbCommand myCommand4 = new OleDbCommand(myQuery4, myConnection2);

            try
            {
                myConnection2.Open();
                myCommand4.ExecuteNonQuery();
                return "Sussesfully Deleted";
            }
            catch (Exception ex)
            {
                return "Course not Deleted, Please Check details and try again " + ex;
            }
            finally
            {
                myConnection2.Close();
            }
        }

        //delete staff
        public static string DeleteStaff(int StaffID)
        {
            OleDbConnection myConnection2 = GetConnection();
            string myQuery7 = "DELETE * FROM Staff WHERE StaffID = " + StaffID + "";
            OleDbCommand myCommand7 = new OleDbCommand(myQuery7, myConnection2);

            try
            {
                myConnection2.Open();
                myCommand7.ExecuteNonQuery();
                return "Sussesfully Deleted";
            }
            catch (Exception ex)
            {
                return "Staff not Deleted, Please Check details and try again " + ex;
            }
            finally
            {
                myConnection2.Close();
            }
        }
        // EMILY ----------------------------------------------------------------------------------------------------
    }
}