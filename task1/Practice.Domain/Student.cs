namespace Practice.Domain;

public class Student : 
    IEquatable<Student>,
    IEquatable<string>

{
    private readonly string _surname;
    private readonly string _name;
    private readonly string _patronymic;
    private readonly string _studyGroup;
    private readonly Course _course;
    public enum Course
    {
        Cs,
        GO,
        Yandex,
        DataSet,
        InfrastructureActivities
    }
    // private readonly string _course;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="surname">Фамилия</param>
    /// <param name="name">Имя</param>
    /// <param name="patronymic">Отчество</param>
    /// <param name="studyGroup">Номер группы</param>
    /// <param name="course">Курс практики</param>
    /// <exception cref="ArgumentNullException">Подан null аргумент</exception>
    public Student(string surname = "", string name = "", string patronymic = "", 
        string studyGroup = "", Course course = default)
    {
        _surname = surname ?? throw new System.ArgumentNullException(nameof(surname));
        _name = name ?? throw new System.ArgumentNullException(nameof(name));
        _patronymic = patronymic ?? throw new System.ArgumentNullException(nameof(patronymic));
        _studyGroup = studyGroup ?? throw new System.ArgumentNullException(nameof(studyGroup));
        _course = course;
    }
    
    // реализация get функций, сокращённая запись
    public string SurnameValue => _surname; // public string Surname { get { return Surname; } }
    public string NameValue => _name;
    public string PatronymicValue => _patronymic;
    public string StudyGroupValue => _studyGroup;
    public Course CourseValue => _course;

    public int GetCourseNumber => _studyGroup[4] - '0'; // номер курса по группе
    // Convert.ToInt32(_studyGroup.Substring(4, 1));

    // set функции не нужны, т.к. readonly
    
    public override string ToString() // переопределение ToString
    {
        return $"[ Surname: {_surname}, Name: {_name}, Patronymic: {_patronymic}," +
               $" StudyGroup: {_studyGroup}, Course: {_course} ]";
    }

    public override int GetHashCode() // переопределение GetHashCode
    {
        return HashCode.Combine(_surname, _name, _patronymic, _studyGroup, _course);
    }

    public bool Equals(Student? other) // для типа Student
    {
        if (other is null)
        {
            return false;
        }

        return _surname.Equals(other._surname)
               && _name.Equals(other._name)
               && _patronymic.Equals(other._patronymic)
               && _studyGroup.Equals(other._studyGroup)
               && _course.Equals(other._course);
    }
    
    public bool Equals(string? @string) // для строк
    {
        return _surname.Equals(@string);
    }
    
    public bool Equals(Enum @enum) // для enum
    {
        return _course.Equals(@enum);
    }
    
    /*public bool Equals(int? other) // для int, вроде не надо
    {
        return _surname.Equals(other);
    }*/

    public bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (obj is Student student)
            return Equals(student);
        if (obj is string @string)
            return Equals(@string);
        if (obj is Course @enum)
            return Equals(@enum);

        return false;
    }
}