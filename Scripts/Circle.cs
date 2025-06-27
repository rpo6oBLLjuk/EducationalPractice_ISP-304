namespace Monte_Karlo.Models
{
    // Класс, представляющий окружность с центром, радиусом и параметром C
    public class Circle
    {
        // Центр окружности (координаты x, y)
        public Point circleCenter = new Point(-3, 1);

        // Радиус окружности (по умолчанию 2)
        public float radius = 2;

        // Дополнительный параметр C (по умолчанию -2)
        public float C = -2;

        // Конструктор по умолчанию - инициализирует значения по умолчанию
        public Circle() { }

        // Конструктор с параметрами для установки центра, радиуса и параметра C
        public Circle(Point circleCenter, float radius, float c)
        {
            this.circleCenter = circleCenter;
            this.radius = radius;
            this.C = c;
        }

        // Переопределение метода Equals для сравнения двух объектов Circle
        // Сравнивает по координатам центра, радиусу и параметру C
        public override bool Equals(object obj)
        {
            return obj is Circle other &&
                circleCenter.X == other.circleCenter.X &&
                circleCenter.Y == other.circleCenter.Y &&
                radius == other.radius &&
                C == other.C;
        }

        // Переопределение GetHashCode для корректной работы с хэш-таблицами и словарями
        // Использует встроенный метод HashCode.Combine для создания хэш-кода на основе всех полей
        public override int GetHashCode()
        {
            return HashCode.Combine(circleCenter.X, circleCenter.Y, radius, C);
        }

        // Переопределение ToString для удобного вывода информации об окружности в виде строки
        public override string ToString()
        {
            return $"CircleCenter: {circleCenter}, Radius: {radius}, C: {C}";
        }
    }
}
