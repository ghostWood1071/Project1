using Project1.DataAcessLayer.Model;
using Project1.DataAcessLayer.Model.Interface;
using Project1.LogicalHandlerLayer;
using Project1.Source;
using Project1.UI.Component;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.UI
{
    class ClassUI: IUIable
    {
        ClassHandler classHandler = new ClassHandler();
        TeacherHandler teacherHandler = new TeacherHandler();
        MajorHandler majorHandler = new MajorHandler();
        SubjectHandler subjectHandler = new SubjectHandler();
        AssignmentHandler assignmentHandler = new AssignmentHandler();

        private Teacher teacher;

        public ClassUI ()
        {

        }

        public ClassUI(Teacher teacher)
        {
            this.teacher = teacher;
        }

        public void Menu()
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            bool exit = true;
            string[] menu = { 
                "1.Thêm lớp mới",
                "2.Sửa thông tin lớp",
                "3.Xóa lớp",
                "4.Hiển thị tất cả lớp",
                 "5.Tìm kiếm lớp",
                "6.Trở lại trang chủ"
            };
            MenuSelector selector = new MenuSelector(menu,"Quản lý lớp học");
            while (exit)
            {
                int mode = selector.Selector();
                switch (mode)
                {
                    case 0:
                        Add();
                        Console.Clear();
                        break;
                    case 1:
                        Update();
                        Console.Clear();
                        break;
                    case 2:
                        Delete();
                        Console.Clear();
                        break;
                    case 3:
                        ShowAll();
                        Console.Clear();
                        break;
                    case 4:
                        Search();
                        Console.Clear();
                        break;
                    case 5:
                        exit = false;
                        Console.Clear();
                        break;
                }
            }
        }

        public void Show()
        {
            
        } // checked

        public void Search()
        {
            Console.CursorVisible = true;
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.Write("Từ khóa: ");
                string input = Console.ReadLine();
                List<Class> classes = this.teacher.Role == (int)UserPermission.HeadSection ? classHandler.GetList(teacher.SubjectID) : classHandler.GetClasses();
                List<Class> result = new List<Class>();
                foreach (var c in classes)
                    if (c.ID.ToLower().Contains(input) ||
                       c.Population.ToString().Contains(input) ||
                       c.SubjectID.Contains(input) ||
                       c.MajorID.Contains(input) ||
                       c.TeacherId.Contains(input) ||
                       c.StartYear.ToString().Contains(input) ||
                       c.EndYear.ToString().Contains(input))
                        result.Add(c);
                PrintTable(result);
                Console.Write("Nhấn esc để thoát");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    Console.CursorVisible = false;
                    exit = true;
                }
            }
        } // checked

        public void ShowAll()
        {
            Console.Clear();
            bool exit = false;
            while (!exit)
            {
                List<Class> classes = this.teacher.Role == (int)UserPermission.HeadSection ? classHandler.GetList(teacher.SubjectID) : classHandler.GetClasses();
                PrintTable(classes);
                Console.Write("Nhấn esc để thoát");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape)
                    exit = true;
            }
        } //checked

        public void PrintTable(List<Class> classes)
        {
            Console.Clear();
            List<Teacher> teachers = teacherHandler.GetList();
            List<Major> majors = majorHandler.GetMajors();
            List<Subject> subjects = subjectHandler.GetSubjects();
            Table table = new Table(90);

            table.PrintLine();
            table.PrintRow("Lớp", "Sĩ số", "Bộ môn", "Chuyên ngành", "Giảng viên CN", "Niên khóa");
            table.PrintLine();
            foreach (var c in classes)
            {
                string tcName = teacherHandler.GetInfo(c.TeacherId, teachers).Name;
                string majorName = majorHandler.GetMajor(c.MajorID, majors).Name;
                string subjectName = subjectHandler.GetSubject(c.SubjectID, subjects).Name;
                table.PrintRow(c.ID, c.Population.ToString(), subjectName, majorName, tcName, c.StartYear+ "-" + c.EndYear);
            }
            table.PrintLine();
        } // checked

        public void Delete()
        {
            List<Class> classes = this.teacher.Role == (int)UserPermission.HeadSection ? classHandler.GetList(teacher.SubjectID) : classHandler.GetClasses();
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                PrintTable(classes);
                string classId = GetID2();
                classHandler.Delete(classId);
                assignmentHandler.Delete(assigment => assigment.ClassID == classId);
                classes.RemoveAt(classHandler.GetIndex(classId, classes));
                Console.Clear();
                PrintTable(classes);
                Console.Write("Nhấn esc để thoát");
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape)
                    exit = true;
            }
        } //checked

        public void Update()
        {
            Console.Clear();
            List<Class> classes = this.teacher.Role == (int)UserPermission.HeadSection ? classHandler.GetList(teacher.SubjectID) : classHandler.GetClasses();
            PrintTable(classes);
            bool exit = false;
            while (!exit)
            {
                string classId = GetID2();
                Class oldInfo = classHandler.GetClasses()[classHandler.GetIndex(classId)];
                Class newInfo = new Class();
                int population = GetPopulation(true);
                string subId = this.teacher.Role == (int)UserPermission.HeadSection ? teacher.SubjectID : GetSubjectId();
                string majorId = GetMajorId();
                string teacherId = GetTeacherId();

                newInfo.ID = oldInfo.ID;

                if (population == 0)
                    newInfo.Population = oldInfo.Population;
                else
                    newInfo.Population = population;

                if (subId == "")
                    newInfo.SubjectID = oldInfo.SubjectID;
                else
                    newInfo.SubjectID = subId;

                if (majorId == "")
                    newInfo.MajorID = oldInfo.MajorID;
                else
                    newInfo.MajorID = majorId;

                if (teacherId == "")
                    newInfo.TeacherId = oldInfo.TeacherId;
                else
                    newInfo.TeacherId = teacherId;

                classHandler.Update(classId, newInfo);
                Console.Clear();
                classes[classHandler.GetIndex(classId, classes)] = newInfo;
                PrintTable(classes);
                Console.Write("Bạn có muốn sửa tiếp không?(nhấn esc để thoát)");
                ConsoleKeyInfo exitStr = Console.ReadKey();
                if (exitStr.Key == ConsoleKey.Escape)
                    exit = true;
            }
        } //checked

        public void Add()
        {
            List<Class> classes = this.teacher.Role == (int)UserPermission.HeadSection ? classHandler.GetList(teacher.SubjectID) : classHandler.GetClasses();
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                PrintTable(classes);

                string classId = GetID();
                int population = GetPopulation();
                string subId = this.teacher.Role == (int)UserPermission.HeadSection ? teacher.SubjectID : GetSubjectId();
                string majorId = GetMajorId();
                string teacherId = GetTeacherId();

                classHandler.Add(new Class(classId, population, subId, majorId, teacherId));
                classes.Add(new Class(classId, population, subId, majorId, teacherId));

                Console.Clear();
                PrintTable(classes);
                Console.Write("Bạn có muốn nhập tiếp không?(nhấn esc để thoát) ");
                ConsoleKeyInfo exitStr = Console.ReadKey();
                if (exitStr.Key == ConsoleKey.Escape)
                    exit = true;
            }
        }//checked

        public string GetID(bool acceptNull = false)
        {
            List<Class> classes = this.teacher.Role == (int)UserPermission.HeadSection ? classHandler.GetList(teacher.SubjectID) : classHandler.GetClasses();
            while (true)
            {
                Console.Write("Mã lớp: ");
                string id = Console.ReadLine();
                if (acceptNull && id == "")
                    return id;
                if (id.Length < 6)
                    Console.WriteLine("Mã lớp gồm 7 kí tự trở lên");
                else if (classHandler.GetClass(id, classes) != null)
                    Console.WriteLine("Lớp đã tồn tại");
                else
                    return id;
            }
        }

        public string GetID2(bool acceptNull = false)
        {
            List<Class> classes = this.teacher.Role == (int)UserPermission.HeadSection ? classHandler.GetList(teacher.SubjectID) : classHandler.GetClasses();
            while (true)
            {
                Console.Write("Mã lớp: ");
                string id = Console.ReadLine();
                if (acceptNull && id == "")
                    return id;
                if (id.Length < 6)
                    Console.WriteLine("Mã lớp gồm 7 kí tự trở lên");
                else if (classHandler.GetClass(id,classes) == null)
                    Console.WriteLine("Lớp không tồn tại");
                else
                    return id;
            }
        }

        public int GetPopulation(bool acceptNull = false)
        {
            while (true)
            {
                Console.Write("Sĩ số lớp: ");
                try
                {
                    int population = int.Parse(Console.ReadLine());
                    if (population < 0)
                        Console.WriteLine("Sĩ số không âm");
                    else if (population < 10)
                        Console.WriteLine("Sĩ số 1 lớp >=10");
                    else
                        return population;
                }
                catch
                {
                    if (acceptNull)
                        return 0;
                    else
                        Console.WriteLine("Sĩ số là một số không phải kí tự");
                }
                
            }
        }

        public string GetSubjectId()
        {
            Console.Clear();
            List<Subject> subjects = subjectHandler.GetSubjects();
            string[] selectItem = new string[subjects.Count];
            for(int i = 0; i<subjects.Count; i++)
            {
                selectItem[i] = i+"."+subjects[i].Name;
            }
            MenuSelector menuSelector = new MenuSelector(selectItem, "Bộ môn");
            return subjects[menuSelector.Selector()].ID;
        }

        public string GetMajorId()
        {
            Console.Clear();
            List<Major> majors = this.teacher.Role == (int)UserPermission.HeadSection ? majorHandler.GetMajors(this.teacher.SubjectID) : majorHandler.GetMajors();
            string[] selectItem = new string[majors.Count];
            for(int i = 0; i<majors.Count; i++)
            {
                selectItem[i] = majors[i].Name;
            }
            MenuSelector menuSelector = new MenuSelector(selectItem, "Chuyên ngành");
            return majors[menuSelector.Selector()].ID;
        }

        public string GetTeacherId()
        {
            Console.Clear();
            List<Teacher> teachers = this.teacher.Role == (int)UserPermission.HeadSection ? teacherHandler.GetList(this.teacher.SubjectID) : teacherHandler.GetList();
            string[] selectItem = new string[teachers.Count];
            for(int i = 0; i< teachers.Count; i++)
            {
                selectItem[i] =teachers[i].ID+" - "+teachers[i].Name;
            }
            MenuSelector menuSelector = new MenuSelector(selectItem, "Giảng viên");
            return teachers[menuSelector.Selector()].ID;
        }
    }
}
