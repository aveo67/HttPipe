# HttPipe
##  Что такое HttPipe?
**HttPipe** - это библиотека которая упрощает работу с **HttpClient**. Основные компоненты библиотеки - это абстрактный пайплайн обработки запросов Http и удобный билдер для сбора этих пайплайнов на любой случай жизни.
Будучи C# разработчиком я давно оценил удобство такого встроенного в .Net инструмента как HttpClient. Однако у него также есть и недостатки. Можно заметить, что часто приходится конфигурировать одинаковые или очень похожие по своей структуре запросы: одинаковая сериализация, похожие строки запросов, одинаковые хэдеры и т.д. Это не что иное как бойлерплейт. Интуитивно в голову приходит идея: "Что если сделать некую обертку для HttpClient которую можно сконфигурировать в виде: `_builder.ConfigurePost().WithIdAsQuery().WithJsonSerialization().WithStringAsResult()`". Данная библиотека и есть реализация данной идеи.

## Как использовать

Прежде чем отправить запрос нужно сконфигурировать пайплайн в билдере, создать экземпляр пайплайна и вызвать соответствующий метод.

``` CSharp
IHttpPipeline<QueryModel, Payload, Result> pipeline = HttpPipe.Create<QueryModel, Payload, Result>(
    builder => builder
    .Configure("localhost", HttpMethod.Get, default) // Задаем эндпоинт, метод запроса и опционально таймаут
    .WithoutAuthentication() // Указываем способ аутентификации
    .WithQuery() // Указываем как будет сформирована строка запроса
    .WithPayloadAsJson() // Указываем способ сериализацие тела запроса
    .WithJsonDeserialization() // Указываем способ десериализации тела ответа
    .AddHeader("If-Modified-Since", "***") // Опционально! Добавляем хэдер если необходимо
    .RemoveHeader("If-Modified-Since") // Опционально! Удаляем лишний хэдер если необходимо
    .AttachRequestStep(new SomeCustomRequestStep()) // Опционально! Добавляем свой шаг подготовки запроса в виде экземпляра IRequestStep, IRequestStep<TPayload>, IRequestStep<TQueryModel, TPayload>
    .AttachRequestStep((request, token) => Task.CompletedTask) // Опционально! Добавляем свой шаг подготовки запроса в виде лямбда-выражения
    .AttachRequestStep((payload, request, token) => Task.CompletedTask) // Опционально! Добавляем свой шаг подготовки запроса в виде лямбда-выражения
    .AttachRequestStep((query, payload, request, token) => Task.CompletedTask) // Опционально! Добавляем свой шаг подготовки запроса в виде лямбда-выражения
    .AttachResponseStep(new SomeCustomResponseStep()) // Опционально! Добавляем свой шаг обработки ответа в виде экземпляра IResponseStep<TResult>, IResponseStep<TPayload, TResult>, IResponseStep<TQueryModel, TPayload, TResult>, 
    .AttachResponseStep((result, request, response, token) => Task.FromResult(default(Result))) // Опционально! Добавляем свой шаг обработки ответа в виде лямбда выражения
    .AttachResponseStep((payload, result, request, response, token) => Task.FromResult(default(Result))) // Опционально! Добавляем свой шаг обработки ответа в виде лямбда выражения
    .AttachResponseStep((query, payload, result, request, response, token) => Task.FromResult(default(Result))) // Опционально! Добавляем свой шаг обработки ответа в виде лямбда выражения
    .ConfigureHttpClient(client => { /* do something with HttpClient */}) // Опционально! Дополнительные манипуляции с HttpClient
);
    
Result result = await pipeline.SendAsync(new QueryModel(), new Payload(), _tokenSource.Token); // Запускаем пайплайн
```

Есть три вида пайплайнов которые отличаются набором типов которые необходимы для отправки запроса и обработки ответов:
`IHttpPipeline<TResult>`
`IHttpPipeline<TPayload, TResult>`
`IHttpPipeline<TQueryModel, TPayload, TResult>`

Кроме того, для методов `get`, `post` и `put` есть отдельные пайплайны которые немного отличаются на этапе конфигурирования:
`IGetHttpPipeline<TResult>`
`IGetHttpPipeline<TQueryModel, TResult>`
`IPostHttpPipeline<TPayload, TResult>`
`IPostHttpPipeline<TQueryModel, Tpayload, TResult>`
`IPutHttpPipeline<TPayload, TResult>`
`IPutHttpPipeline<TQueryModel, TPayload, TResult>`

## Конфигурирование пайплайнов
### Инициализация цепочки конфигурирования
### Шаг аутентификации
### Шаг создания строки запроса
### Шаг сериализации
### Шаг десериализации
### Дополнительные шаги: окончательная настройка HttpClient
### Дополнительные шаги: шаги собственной реализации
### Дополнительные шаги: лямбда-шаги
### Доступные методы расширения
### Собственные методы расширения

## Создание экземпляров пайплайнов
### При помощи встроенной фабрики
### При помощи класса-конфигуратора
### Интеграция с DI

## Unity

## Зависимости
Newtonsoft.Json 13.0.4
WebSerializer 1.3.0

## Установка

## Лицензия
This library is under the MIT License.