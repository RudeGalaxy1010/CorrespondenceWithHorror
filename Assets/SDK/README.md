# Привет, разработчик![](https://github.com/blackcater/blackcater/raw/main/images/Hi.gif) 
### SDK для встраивания веб-функций от GeekPlay

### Как с ним работать?
##### 1. Импорт

    Скачайте данные файлы и поместите в отдельную папку своего проекта

##### 2. Сохранения

    Поместите все значения, которые хотите сохранить внутрь класса PlayerData в скрипте Init
    Вызывайте функцию Init.Save() там, где хотите сохранять данные
    Если хотите загружать данные, то используйте функцию Init.Load()

##### 3. Межстраничная реклама

    Там, где хотите показывать межстраничную рекламу, используйте функцию Init.ShowInterstitialAd()

##### 4. Реклама с вознаграждением

    Чтобы вызвать рекламу с вознаграждением, используйте функцию Init.ShowRewardedAd(idOrTag)
    В качестве аргумента для этой функции используйте строковый TAG вашей награды
    Внутри функции OnRewarded() по примеру вставьте получение награды (то, что игрок получит после просмотра рекламы)

##### 5. Лидерборд

    Чтобы сохранить данные в лидерборд вызовите функцию Init.Leaderboard()
    Первым аргументом укажите название лидерборда в формате строки
    Вторым аргументом укажите значение (число), которое будет отображаться у игрока в лидерборде

##### 6. "Другие игры"

    Повесьте функцию Init.OpenOtherGames() на кнопку открытия "Других игр"

 ##### 7. Просьба отзыва

    Вызовите функцию Init.RateGame() там, где хотите попросить игрока оценить игру

 ##### 8. Мобильные устройства
 
    Если у вас есть что-то, что должно выключиться/включиться на мобильных устройствах, используйте
    для этого переменную mobile из скрипта Init
