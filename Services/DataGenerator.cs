﻿using System;
using Furmanov.Data.Data;

namespace Furmanov.Services
{
	public class DataGenerator
	{
		#region Fields
		private readonly Random _rnd = new Random();
		private static readonly string[] Names =
		{
			"Абрам",
			"Аваз",
			"Август",
			"Авдей",
			"Автандил",
			"Адам",
			"Адис",
			"Адольф",
			"Адриан",
			"Азарий",
			"Аким",
			"Александр",
			"Алексей",
			"Альберт",
			"Альфред",
			"Амадей",
			"Амадеус",
			"Амаяк",
			"Анатолий",
			"Ангел",
			"Андоим",
			"Андрей",
			"Аникита",
			"Антон",
			"Арам",
			"Арий",
			"Аристарх",
			"Аркадий",
			"Арно",
			"Арнольд",
			"Арон",
			"Арсен",
			"Артем",
			"Артемий",
			"Артур",
			"Архип",
			"Аскольд",
			"Афанасий",
			"Ахмет",
			"Ашот",
			"Бенедикт",
			"Берек",
			"Бернар",
			"Богдан",
			"Боголюб",
			"Болеслав",
			"Бонифаций",
			"Борис",
			"Борислав",
			"Боян",
			"Бронислав",
			"Бруно",
			"Вадим",
			"Валентин",
			"Валерий",
			"Вальтер",
			"Василий",
			"Велизар",
			"Венедикт",
			"Вениамин",
			"Виктор",
			"Вилен",
			"Вилли",
			"Вильгельм",
			"Виссарион",
			"Виталий",
			"Витаутас",
			"Витольд",
			"Владимир",
			"Владислав",
			"Владлен",
			"Володар",
			"Вольдемар",
			"Всеволод",
			"Вячеслав",
			"Гавриил",
			"Гарри",
			"Гастон",
			"Геннадий",
			"Генрих",
			"Георгий",
			"Геральд",
			"Герасим",
			"Герман",
			"Глеб",
			"Гордей",
			"Гордон",
			"Градимир",
			"Григорий",
			"Гурий",
			"Даниил",
			"Денис",
			"Джордан",
			"Дмитрий",
			"Дональд",
			"Донат",
			"Донатос",
			"Дорофей",
			"Евгений",
			"Евграф",
			"Евдоким",
			"Евстафий",
			"Егор",
			"Елизар",
			"Елисей",
			"Емельян",
			"Ермолай",
			"Ерофей",
			"Ефим",
			"Ефимий",
			"Ефрем",
			"Ждан",
			"Жорж",
			"Захар",
			"Захария",
			"Зигмунд",
			"Зиновий",
			"Ибрагим",
			"Иван",
			"Игорь",
			"Измаил",
			"Израиль",
			"Илиан",
			"Илларион",
			"Илья",
			"Иннокентий",
			"Иосиф",
			"Ираклий",
			"Иржи",
			"Исай",
			"Казимир",
			"Карен",
			"Карл",
			"Кирилл",
			"Клавдий",
			"Клемент",
			"Клод",
			"Кондрат",
			"Конкордий",
			"Константин",
			"Кузьма",
			"Лазарь",
			"Леван",
			"Леонард",
			"Леонид",
			"Леонтий",
			"Леопольд",
			"Лука",
			"Любомир",
			"Людвиг",
			"Люсьен",
			"Мадлен",
			"Макар",
			"Максим",
			"Максимилиан",
			"Мануил",
			"Марат",
			"Мариан",
			"Марк",
			"Матвей",
			"Мераб",
			"Милан",
			"Мирон",
			"Мирослав",
			"Михаил",
			"Мичлов",
			"Модест",
			"Моисей",
			"Мурат",
			"Муслим",
			"Назар",
			"Назарий",
			"Натан",
			"Наум",
			"Никита",
			"Никифор",
			"Николай",
			"Никон",
			"Нисон",
			"Нифонт",
			"Олан",
			"Олег",
			"Олесь",
			"Онисим",
			"Орест",
			"Осип",
			"Оскар",
			"Павел",
			"Парамон",
			"Петр",
			"Платон",
			"Порфирий",
			"Прохор",
			"Равиль",
			"Радомир",
			"Раис",
			"Раймонд",
			"Ратмир",
			"Рафаил",
			"Рафик",
			"Рашид",
			"Ренольд",
			"Рифат",
			"Ричард",
			"Роберт",
			"Родион",
			"Ролан",
			"Роман",
			"Ростислав",
			"Рубен",
			"Рудольф",
			"Руслан",
			"Рустам",
			"Савва",
			"Самсон",
			"Святослав",
			"Севастьян",
			"Северин",
			"Семен",
			"Серафим",
			"Сергей",
			"Сократ",
			"Соломон",
			"Спартак",
			"Стакрат",
			"Станислав",
			"Степан",
			"Стивен",
			"Стоян",
			"Таис",
			"Талик",
			"Тамаз",
			"Тарас",
			"Тельман",
			"Теодор",
			"Терентий",
			"Тибор",
			"Тигран",
			"Тигрий",
			"Тимофей",
			"Тимур",
			"Тихон",
			"Трифон",
			"Трофим",
			"Ульманас",
			"Устин",
			"Фаддей",
			"Федор",
			"Феликс",
			"Феодосий",
			"Фидель",
			"Филимон",
			"Филипп",
			"Флорентий",
			"Фома",
			"Франц",
			"Фридрих",
			"Харитон",
			"Христиан",
			"Христос",
			"Христофор",
			"Эдвард",
			"Эдуард",
			"Эльдар",
			"Эмиль",
			"Эммануил",
			"Эраст",
			"Эрик",
			"Эрнест",
			"Юлиан",
			"Юрий",
			"Юхим",
			"Яким",
			"Яков",
			"Яромир",
			"Ярослав",
			"Ясон",
		};
		private static readonly string[] Surnames =
		{
			"Смирнов",
			"Иванов",
			"Кузнецов",
			"Соколов",
			"Попов",
			"Лебедев",
			"Козлов",
			"Новиков",
			"Морозов",
			"Петров",
			"Волков",
			"Соловьёв",
			"Васильев",
			"Зайцев",
			"Павлов",
			"Семёнов",
			"Голубев",
			"Виноградов",
			"Богданов",
			"Воробьёв",
			"Фёдоров",
			"Михайлов",
			"Беляев",
			"Тарасов",
			"Белов",
			"Комаров",
			"Орлов",
			"Киселёв",
			"Макаров",
			"Андреев",
			"Ковалёв",
			"Ильин",
			"Гусев",
			"Титов",
			"Кузьмин",
			"Кудрявцев",
			"Баранов",
			"Куликов",
			"Алексеев",
			"Степанов",
			"Яковлев",
			"Сорокин",
			"Сергеев",
			"Романов",
			"Захаров",
			"Борисов",
			"Королёв",
			"Герасимов",
			"Пономарёв",
			"Григорьев",
			"Лазарев",
			"Медведев",
			"Ершов",
			"Никитин",
			"Соболев",
			"Рябов",
			"Поляков",
			"Цветков",
			"Данилов",
			"Жуков",
			"Фролов",
			"Журавлёв",
			"Николаев",
			"Крылов",
			"Максимов",
			"Сидоров",
			"Осипов",
			"Белоусов",
			"Федотов",
			"Дорофеев",
			"Егоров",
			"Матвеев",
			"Бобров",
			"Дмитриев",
			"Калинин",
			"Анисимов",
			"Петухов",
			"Антонов",
			"Тимофеев",
			"Никифоров",
			"Веселов",
			"Филиппов",
			"Марков",
			"Большаков",
			"Суханов",
			"Миронов",
			"Ширяев",
			"Александров",
			"Коновалов",
			"Шестаков",
			"Казаков",
			"Ефимов",
			"Денисов",
			"Громов",
			"Фомин",
			"Давыдов",
			"Мельников",
			"Щербаков",
			"Блинов",
			"Колесников",
			"Карпов",
			"Афанасьев",
			"Власов",
			"Маслов",
			"Исаков",
			"Тихонов",
			"Аксёнов",
			"Гаврилов",
			"Родионов",
			"Котов",
			"Горбунов",
			"Кудряшов",
			"Быков",
			"Зуев",
			"Третьяков",
			"Савельев",
			"Панов",
			"Рыбаков",
			"Суворов",
			"Абрамов",
			"Воронов",
			"Мухин",
			"Архипов",
			"Трофимов",
			"Мартынов",
			"Емельянов",
			"Горшков",
			"Чернов",
			"Овчинников",
			"Селезнёв",
			"Панфилов",
			"Копылов",
			"Михеев",
			"Галкин",
			"Назаров",
			"Лобанов",
			"Лукин",
			"Беляков",
			"Потапов",
			"Некрасов",
			"Хохлов",
			"Жданов",
			"Наумов",
			"Шилов",
			"Воронцов",
			"Ермаков",
			"Дроздов",
			"Игнатьев",
			"Савин",
			"Логинов",
			"Сафонов",
			"Капустин",
			"Кириллов",
			"Моисеев",
			"Елисеев",
			"Кошелев",
			"Костин",
			"Горбачёв",
			"Орехов",
			"Ефремов",
			"Исаев",
			"Евдокимов",
			"Калашников",
			"Кабанов",
			"Носков",
			"Юдин",
			"Кулагин",
			"Лапин",
			"Прохоров",
			"Нестеров",
			"Харитонов",
			"Агафонов",
			"Муравьёв",
			"Ларионов",
			"Федосеев",
			"Зимин",
			"Пахомов",
			"Шубин",
			"Игнатов",
			"Филатов",
			"Крюков",
			"Рогов",
			"Кулаков",
			"Терентьев",
			"Молчанов",
			"Владимиров",
			"Артемьев",
			"Гурьев",
			"Зиновьев",
			"Гришин",
			"Кононов",
			"Дементьев",
			"Ситников",
			"Симонов",
			"Мишин",
			"Фадеев",
			"Комиссаров",
			"Мамонтов",
			"Носов",
			"Гуляев",
			"Шаров",
			"Устинов",
			"Вишняков",
			"Евсеев",
			"Лаврентьев",
			"Брагин",
			"Константинов",
			"Корнилов",
			"Авдеев",
			"Зыков",
			"Бирюков",
			"Шарапов",
			"Никонов",
			"Щукин",
			"Дьячков",
			"Одинцов",
			"Сазонов",
			"Якушев",
			"Красильников",
			"Гордеев",
			"Самойлов",
			"Князев",
			"Беспалов",
			"Уваров",
			"Шашков",
			"Бобылёв",
			"Доронин",
			"Белозёров",
			"Рожков",
			"Самсонов",
			"Мясников",
			"Лихачёв",
			"Буров",
			"Сысоев",
			"Фомичёв",
			"Русаков",
			"Стрелков",
			"Гущин",
			"Тетерин",
			"Колобов",
			"Субботин",
			"Фокин",
			"Блохин",
			"Селиверстов",
			"Пестов",
			"Кондратьев",
			"Силин",
			"Меркушев",
			"Лыткин",
			"Туров",
		};
		private static readonly string[] Positions =
		{
			"Директор",
			"Финансовый директор",
			"Главный бухгалтер",
			"Главный диспетчер",
			"Главный инженер",
			"Главный конструктор",
			"Главный металлург",
			"Главный метролог",
			"Главный механик",
			"Главный сварщик",
			"Главный специалист по защите информации",
			"Главный технолог",
			"Главный энергетик",
			"Директор гостиницы",
			"Директор котельной",
			"Директор по связям с инвесторами",
			"Директор типографии",
			"Заведующая машинописным бюро",
			"Заведующий архивом",
			"Заведующий бюро пропусков",
			"Заведующий жилым корпусом пансионата",
			"Заведующий камерой хранения",
			"Заведующий канцелярией",
			"Заведующий копировально-множительным бюро",
			"Заведующий научно-технической библиотекой",
			"Заведующий общежитием",
			"Заведующий производством",
			"Заведующий складом",
			"Заведующий столовой",
			"Заведующий фотолабораторией",
			"Заведующий хозяйством",
			"Заведующий экспедицией",
			"Заместитель директора по капитальному строительству",
			"Заместитель директора по коммерческим вопросам",
			"Заместитель директора по связям с общественностью",
			"Заместитель директора по управлению персоналом",
			"Корпоративный секретарь акционерного общества",
			"Мастер участка",
			"Менеджер",
			"Менеджер по персоналу",
			"Менеджер по рекламе",
			"Менеджер по связям с инвесторами",
			"Менеджер по связям с общественностью",
			"Начальник автоколонны",
			"Начальник гаража",
			"Начальник мастерской",
			"Начальник инструментального отдела",
			"Начальник исследовательской лаборатории",
			"Начальник лаборатории по организации труда и управления производством",
			"Начальник лаборатории социологии труда",
			"Начальник лаборатории технико-экономических исследований",
			"Начальник нормативно-исследовательской лаборатории по труду",
			"Начальник отдела автоматизации и механизации производственных процессов",
			"Начальник отдела информации",
			"Начальник отдела кадров",
			"Начальник отдела капитального строительства",
			"Начальник отдела комплектации оборудования",
			"Начальник отдела контроля качества",
			"Начальник отдела маркетинга",
			"Начальник отдела материально-технического снабжения",
			"Начальник отдела организации и оплаты труда",
			"Начальник отдела охраны окружающей среды",
			"Начальник отдела охраны труда",
			"Начальник отдела патентной и изобретательской работы",
			"Начальник отдела подготовки кадров",
			"Начальник отдела по связям с инвесторами",
			"Начальник отдела (лаборатории, сектора) по защите информации",
			"Начальник отдела по связям с общественностью",
			"Начальник отдела сбыта",
			"Начальник отдела социального развития",
			"Начальник отдела стандартизации",
			"Начальник планово-экономического отдела",
			"Начальник производственного отдела",
			"Начальник ремонтного цеха",
			"Начальник смены",
			"Начальник технического отдела",
			"Начальник финансового отдела",
			"Начальник хозяйственного отдела",
			"Начальник центральной заводской лаборатории",
			"Начальник цеха опытного производства",
			"Начальник юридического отдела",
			"Руководитель группы по инвентаризации строений и сооружений",
			"Администратор",
			"Аналитик",
			"Аудитор",
			"Аукционист",
			"Биржевой маклер",
			"Брокер",
			"Брокер торговый",
			"Бухгалтер",
			"Бухгалтер-ревизор",
			"Дилер",
			"Диспетчер",
			"Документовед",
			"Инженер",
			"Инженер-конструктор",
			"Инженер-лаборант",
			"Инженер по автоматизации и механизации производственных процессов",
			"Инженер по автоматизированным системам управления производством",
			"Инженер по защите информации",
			"Инженер по инвентаризации строений и сооружений",
			"Инженер по инструменту",
			"Инженер по качеству",
			"Инженер по комплектации оборудования",
			"Инженер по метрологии",
			"Инженер по надзору за строительством",
			"Инженер по наладке и испытаниям",
			"Инженер по научно-технической информации",
			"Инженер по нормированию труда",
			"Инженер по организации труда",
			"Инженер по организации управления производством",
			"Инженер по охране труда",
			"Инженер по патентной и изобретательской работе",
			"Инженер по подготовке кадров",
			"Инженер по подготовке производства",
			"Инженер по ремонту",
			"Инженер по стандартизации",
			"Инспектор по кадрам",
			"Инспектор по контролю за исполнением поручений",
			"Инспектор фонда",
			"Инструктор-дактилолог",
			"Консультант по налогам и сборам",
			"Лаборант",
			"Математик",
			"Методист по физической культуре",
			"Механик",
			"Оценщик",
			"Оценщик интеллектуальной собственности",
			"Переводчик",
			"Переводчик-дактилолог",
			"Переводчик синхронный",
			"Профконсультант",
			"Психолог",
			"Социолог",
			"Специалист по защите информации",
			"Специалист по кадрам",
			"Специалист по маркетингу",
			"Специалист по оценке соответствия лифтов требованиям безопасности",
			"Специалист по связям с инвесторами",
			"Специалист по промышленной безопасности подъемных сооружений",
			"Специалист по связям с общественностью",
			"Техник",
			"Техник-конструктор",
			"Техник-лаборант",
			"Техник по защите информации",
			"Техник по инвентаризации строений и сооружений",
			"Техник по инструменту",
			"Техник по метрологии",
			"Техник по наладке и испытаниям",
			"Техник по планированию",
			"Техник по стандартизации",
			"Техник по труду",
			"Техник-программист",
			"Техник-технолог",
			"Товаровед",
			"Транспортный экспедитор",
			"Физиолог",
			"Художник",
			"Шеф-инженер",
			"Экономист",
			"Экономист по планированию",
			"Экономист по сбыту",
			"Экономист по труду",
			"Экономист по финансовой работе",
			"Эксперт по оценке соответствия лифтов требованиям безопасности",
			"Эксперт",
			"Эксперт дорожного хозяйства",
			"Эксперт по промышленной безопасности подъемных сооружений",
			"Юрисконсульт",
			"Агент",
			"Агент коммерческий",
			"Агент по закупкам",
			"Агент по продаже недвижимости",
			"Агент по снабжению",
			"Агент рекламный",
			"Агент страховой",
			"Агент торговый",
			"Архивариус",
			"Ассистент инспектора фонда",
			"Дежурный бюро пропусков",
			"Делопроизводитель",
			"Инкассатор",
			"Калькулятор",
			"Кассир",
			"Кодификатор",
			"Комендант",
			"Коммивояжер",
			"Копировщик",
			"Крупье",
			"Машинистка",
			"Нарядчик",
			"Оператор диспетчерской движения и погрузочно-разгрузочных работ",
			"Оператор диспетчерской службы",
			"Оператор по диспетчерскому обслуживанию лифтов",
			"Секретарь-машинистка",
			"Секретарь незрячего специалиста",
			"Секретарь руководителя",
			"Секретарь-стенографистка",
			"Статистик",
			"Стенографистка",
			"Табельщик",
			"Таксировщик",
			"Учетчик",
			"Хронометражист",
			"Чертежник",
			"Экспедитор",
			"Экспедитор по перевозке грузов",
			"Ученый секретарь",
			"Главный научный сотрудник",
			"Ведущий научный сотрудник",
			"Старший научный сотрудник",
			"Научный сотрудник",
			"Младший научный сотрудник",
			"Заведующий аспирантурой",
			"Заведующий отделом стандартизации",
			"Заведующий отделом научно-технической информации",
			"Заведующий планово-экономическим отделом",
			"Заведующий отделом кадров",
			"Заведующий техническим архивом",
			"Заведующий фотолабораторией",
			"Ведущий инженер",
			"Ведущий экономист",
			"Инженер",
			"Экономист",
			"Переводчик",
			"Художник",
			"Техник",
			"Лаборант",
			"Главный конструктор проекта",
			"Главный инженер проекта. Главный архитектор проекта",
			"Главный ландшафтный архитектор проекта",
			"Заведующий конструкторским отделом",
			"Заведующий чертежно-копировальным бюро",
			"Ведущий конструктор",
			"Инженер-проектировщик",
			"Архитектор",
			"Ландшафтный архитектор",
			"Техник-проектировщик",
			"Чертежник-конструктор",
			"Руководитель подразделения",
			"Главный редактор",
			"Научный редактор",
			"Редактор",
			"Технический редактор",
			"Художественный редактор",
			"Выпускающий",
			"Младший редактор",
			"Корректор",
		};
		#endregion
		public UserDto GenUser()
		{
			var user = new UserDto
			{
				Name = GenName(),
				RoleId = (int)GenRole(),
				Password = "1"
			};

			user.Login = $"{user.Name.Replace(" ", "_")}".ToEn();
			
			var random = _rnd.Next(1000);
			var mail = random < 300 ? "@mail.ru"
				: random < 700 ? "@gmail.com"
				: "@yandex.ru";

			user.Email = user.Login + mail;

			return user;
		}
		private string GenName()
		{
			var name = Names[_rnd.Next(Names.Length)];
			var surname = Surnames[_rnd.Next(Surnames.Length)];
			return $"{name} {surname}";
		}
		private Role GenRole()
		{
			var random = _rnd.Next(1000);
			if (random < 100) return Role.Admin;
			if (random < 200) return Role.Director;
			if (random < 300) return Role.ProjectManager;
			return Role.Manager;
		}

		public EmployeeDto GenEmployee()
		{
			var employee = new EmployeeDto
			{
				Name = GenName(),
				Position = Positions[_rnd.Next(Positions.Length)],
				Card = $"{Num4} {Num4} {Num4} {Num4}",
				Phone = $"8 (9{Num2})-{Num3}-{Num2}-{Num2}",
				Salary = _rnd.Next(10, 40) * 5000,
			};

			return employee;
		}
		private string Num2 => $"{_rnd.Next(99):00}";
		private string Num3 => $"{_rnd.Next(999):000}";
		private string Num4 => $"{_rnd.Next(9999):0000}";
	}
}
