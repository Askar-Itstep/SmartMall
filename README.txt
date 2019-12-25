1. Для демонстрации изображений необходимо создать папку в Обозревателе решений 
   Image и добавить в нее  (из Обозревателя) все фотографии из home2_SmallMart\SmallMart\Image\..

2. Пароли:
a)клиенты
//string tempLogin = "ivan_i@yandex.ru", tempPass = "ivan7013892614";   
//string tempLogin = "petr_p@yandex.ru", tempPass = "petr123";

б) Admin (полный доступ по табл. в WindowStaff, + нет скрытых полей)
//string tempLogAdmin = "chingo@yahoo.com", tempPassAdmin = "chingo45";

в) продавец (ограничения по некоторым данным, при этом статистика скрыта-откроется по вызову из Menu)
//string cellerLogin = "inga@mail.ru", cellerPass = "inga36";

3. В проекте (в WindowStaff) используется представление БД, поэтому при создании модели требуется установить
галку в подключениии таблиц и представлений.
