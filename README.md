# hackathon3_command_2 - Команда 2 на Хакатоне в РосКвартал, Петрозаводск. Начало разработки 20.11.2017г. Сдача проекта 16.12.2017г.
Разработан интернет-магазин и CRM система управления пиццерией Dominos Pizza.

Реализовано:
- внешний сайт с каталогом продукции
- возможность фильтрации по ингридиентам
- дополнительная информация о компании, вакансии, фидбек с возможностью прикрепления файлов
- личный кабинет пользователя
- корзина с возможностью редактирования заказа и оплаты на сайте или курьеру
- чат пользователя с менеджером по продажам
- при входе система определяет роль пользователя и перенаправляет его на соответствующую страницу
- в системе предусмотрено 5 ролей: пользователь (покупатель), администратор, менеджер по продажам, повар, курьер
- интерфейс администратора содержит инструменты для управления сотрудниками (добавление, редактирование, удаление), заглушки общей статистики пиццерии и редактирования продукции, реализован вывод активных заказов (т.е. всех, кроме завершенных и отмененных)
- интерфейс менеджера по продажам содержит вывод заказов со статусом "в обработке" и подразумевает проверку на корректность ввода данных и утверждение заказа для дальнейшего перенаправления его "на кухню"
- интерфейс повара содержит информацию о заказах (перечне продукции и кол-ве) и подразумевает передачу выполненного заказа "в доставку"
- интерфейс курьера содержить информацию о заказах переданных в доставку и дает возможность выбрать заказы в работу курьеру, которые переходят в его личный список выполняемых заказов. Курьер распечатывает чек из системы и передает его покупателю вместе с заказом. После доставки курьер завершает заказ и заказ архивируется.

Проект представлен в ознакомительных целях, любое копирование, использование и распространение без согласия авторов запрещено.