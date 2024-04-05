# New-Finance

#### Video Demo: youtube link

## Überblick

In unserem Unternehmen arbeiten wir mit einer Software, welche sich mit der Verbuchung von Wertpapieren befasst. Da ich in Zukunft in diesem Bereich mitwirken möchte, habe ich mich in meinem Projekt dazu entschieden, das in dem Kurs bearbeitete C$50Finance in unsere bereits verwendeten Technologien zu implementieren.

Ich habe mich also erstmal mit den Technologien und Sprachen befasst, die wir bei uns verwenden. Das sind im Wesentlichen C# im Backend und Typescript im Frontend mit Angular.

## Vorgehen

Als erstes habe ich mich mit dem Thema Objektorientierung beschäftigt, insbesondere mit den Programmiersprachen TypeScript und C#.

### Backend

Danach habe ich mich eingehender mit dem Backend befasst. Über Tutorials habe ich beispielsweise Grundlagen in .NET, C# oder WebAPI erlangt und mich über Google in für mich spezifische Themen eingearbeitet. Als Datenbank habe ich PostgreSQL verwendet.

Als "Vermittler" zwischen meinen Klassen und PostgreSQL habe ich Entity Framework verwendet. Ich möchte mit csharp mit der Datenbank arbeiten, ohne direkt SQL zu schreiben.

Im Backend habe ich die verschiedenen GET und POST Routen in Anlehnung an das Original-CS50 FInance eingebaut, aber jeweils so, dass nur die Daten als JSON vom Backend versendet werden.

Daraufhin habe ich die Routen des Backends alle mit Insomnia als HTTP Client getestet

Für die Authentifizierung habe ich im Backend JWT Tokens erzeugt, die bei jedem Login zum Client gesendet werden, und die dann vom Client bei jeder Anfrage mitgesendet werden müssen.

Die Aktienpreise habe ich von einer anderen Webseite bezogen ->

### Frontend

Für das Frontend nutzen wir Angular als Open- Source- Framework und zur Integration mit dem Backend- Service HTTP-Client-Module. Diese rufen die Daten vom Server ab oder senden sie. Der API- Client (apiservice) vereinfacht die Kommunikation dit der API. Der HTTP- Client sendet z.B. eine GET-, oder POST- Anfrage an die API, das heißt die Routen werden vom Server abgerufen. Die Antwort wird verarbeitet und dann???

Außderdem nutzt Angular eine Router- Bibliothek die es ermöglicht, zwischen verschiedenen Ansichten innerhalb einer Single Page Application (SPA) zu navigieren, ohne die Seite jedesmal neu zu laden (in C$50Finance wurde Server Side Rendering (SSR) genutzt).

Angular verwendet Komponenten, wobei jede Komponente aus einer HTML- Vorlage, CSS- Styles und TypeScript-Code besteht. Diese Kombination definiert eine Funktion in der Anwendung. Die Anwendung wird so in einzelne, wiederverwendbare Teile zerlegt. Dadurch lässt sich der einzelne Teil zum Beispiel einfacher testen, außerdem können Komponenten wiederverwendet werden.

Als Programmiersprache verwende ich TypeScript um die Benutzeroberfläche zu definieren, HTML um das Verhalten der Anwendung zu implementieren und CSS um das Aussehen der Anwendung für den Enduser zu gestalten.

Um in Angular die Formular zu verwalten, nutze ich Reactive Forms. Diese werden von der dazugehörenden TypeScript- Klasse gesteuert. Ich habe FormGroup und FormControl verwendet. Des Weiteren habe ich Reactive Forms Validierungsregeln verwendet, z.B. Required, Minlenght und Pattern (damit nur positive Ganzzahlen bei der Stückzahl eingegeben werden können).
Außerdem habe ich die Farbe der Border bei den Eingabefeldern je nach valid oder invalid Eingabe angepasst. Dementsprechend habe ich bei einer invalid Eingabe eine Fehlermeldung ausgeben lassen. Die Submit- Buttons sind so lange gesperrt, bis die Formular korrekt eingegeben wurden, also gültig waren.

### Neue Funktionen

Folgende neue Funktionen habe ich eingebaut:

- Zeigen der tatsächlichen Aktien-Namen
- Kaufen und Verkaufen ist auch direkt aus der Depotübersicht möglich
- Keine Nutzung von Apology-Seiten sondern Einblenden von Fehlern vom Backend in eigenen Boxen
- Einblenden von "Loading"-Meldungen während des Wartens auf die Backend-Antworten

## Detailbeschreibung Backend

| File                                         | Description                                                                                |
| -------------------------------------------- | ------------------------------------------------------------------------------------------ |
| FinanceContext.cs                            | This defines the mapping between my 2 classes and the database tables. I am using 2 tables |
| AktienKursAbfrager.cs                        | This fetches stock names and prices from the external website                              |
| CustomRouteHandlerBuilder.cs                 | This is a module i found on google so that I can use validation attributes for MinimalAPI  |
| DataValidator.cs                             | see above                                                                                  |
| _Jwt_                                        | Code proposed by google and AI to cover the generation and signing of JWT tokens           |
| MatchPropertyAttribute                       | Custom attribute to support regular expression (proposed by AI)                            |
| Folder migrations                            | This is automatically generated by EF                                                      |
| Benutzer.cs                                  | My model class to describe what a user is                                                  |
| Transaktion.cs                               | Same approach for the transactions of stocks                                               |
| \*Request.cs                                 | classes to describe the format of incoming request to the API                              |
| *Response.cs, *Result.cs PortfolioEintrag.cs | classes to describe the format of outgoing responses from the API                          |
| Program.cs                                   | Main class that starts the webserver and registers all routes that the API will respond to |
| docker-compose.yml                           | proposed by AI, this sets up a docker container to run PostgreSQL                          |

## Detailbeschreibung Frontend

The client was generated with Angular CLI as template. I will now describe all files, that I added or modified significantly

| File        | Description                                                   |
| ----------- | ------------------------------------------------------------- |
| styles.scss | For some global styles to indicated valid/invalid form fields |
| api.service.ts | This has all the http get and post requests to the routes from the backend . It also remembers if the user is logged in and stores/receives the JWT token to/from the Browser LocalStorage |
| app.routes.ts | This defines all routes that the user can navigate to and which component should be used for it. E.g. when the user goes to the /sell route, the SellComponent will be shown |
| Register Component | Renders a form to create a new user. Does all the validation and then submits the request. It reroutes to login on success |
| Login Component | Equivalent to register, reroutes to portfolio |
| Quote component | Lets the user enter a symbol and submit a quote request. Will display an error if there is an error server response, otherwise will display the price and is ready for the next request |
| Buy Component | This includes the buy form where the user can input a symbol and quantity, it validates the inputs before the user can submit the buy request. It reroutes to the portfolio page in case of success or shows an error message, if the server responds with an error |
| Sell Component | It fetches the symbols of the stocks in the users posession upon load and populates the select input with it. Then it works equivalent to the buy component |
| History Component | On load, it fetches all transactions from the API and display them as table |
| Portfolio Component | On load, it fetches all open positions aggregated from the transactions and the amount of cash from the server, calculates the total values and displays the data in a table. It also allows to directly buy more or sell the stocks for each line of the table |
| Header Component | It display the navbar and it checks if the user is logged in or not to render only the relevant links |
| Footer Component | It just displays a static information about the data source of the stock prices |

