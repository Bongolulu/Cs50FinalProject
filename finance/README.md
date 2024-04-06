# New Finance

#### Video Demo: [youtube link](https://youtu.be/LVS-Z2Ps2ic)

## Overview

In our company, we work with software that deals with the accounting of securities. As I would like to be involved in this area in the future, I decided to implement the C$50Finance I worked on in your course into the technologies we already use.

So I started by looking at the technologies and languages that we use. These are essentially C# in the backend and Typescript in the frontend with Angular.

## Procedure

First, I learned more about object orientation, in particular with the programming languages TypeScript and C#. I also learned more about HTML and CSS using a website called "The Odin Project" (https://www.theodinproject.com/paths/foundations/courses/foundations).

### Backend

After that, I took a closer look at the backend. For example, I learned the basics of .NET, C# and WebAPI via tutorials and familiarized myself with topics specific to me via Google. I used PostgreSQL as my database.

I used Entity Framework as a "mediator" between my classes and PostgreSQL. I want to work with the database using C# without writing SQL directly.

In the backend, I implemented the various GET and POST routes based on the original C$50 Finance, but in such a way that only the data is sent as JSON from the backend.

I tested all the backend routes with Insomnia as the HTTP client.

For authentication, the backend is creating JWT tokens, which are sent to the client with every login and which then have to be sent by the client with every request.

I obtained the share prices from another website (https://site.financialmodelingprep.com/).

### Frontend

For the frontend, we use Angular as an open source framework using its HTTP client modules for integration with the backend service. This sends and retrieves data from the server. The API client (apiservice) simplifies communication with the API. The HTTP client sends, for example, a GET or POST request to the API, i.e. the routes are retrieved from the server. The response is processed and then displayed accordingly.

In addition, Angular uses a router library that makes it possible to navigate between different views within a Single Page Application (SPA) without reloading the page each time (Server Side Rendering (SSR) was used in C$50Finance).

Angular uses components, where each component consists of an HTML template, CSS styles and TypeScript code. This combination defines a function in the application. The application is thus broken down into individual parts. This makes it easier to test these parts, for example, and components can also be reused.

As a programming language, Angular is based on TypeScript to define the behaviour of the application, but I also had to apply HTML and CSS to design the appearance of the application for the end user. It is based on the original CS50 design but I had to adapt it to be compatible with the SPA approach and also added CSS styles, e.g. for validation.

To manage the forms in Angular, I use Reactive Forms. These are controlled by the corresponding TypeScript class. I used FormGroup and FormControl. Reactive Forms also allow the configuration of validation rules, e.g. Required, Minlenght and Pattern (so that only positive integers can be entered in the quantity).
I have also changed the color of the borders of the input fields depending on whether the input is valid or invalid. Accordingly, I have displayed an error message for an invalid entry. The submit buttons are blocked until the respective form has been entered correctly, i.e. is valid.

### New functions

I have added the following new functions:

- Show the actual share names.
- Buying and selling is also possible directly from the portfolio overview.
- No use of apology pages but display of errors from the backend in special boxes directly on the current page.
- Display of "Loading" messages while waiting for the backend responses.

## Detailed description Backend

| File                                         | Description                                                                                 |
| -------------------------------------------- | ------------------------------------------------------------------------------------------- |
| FinanceContext.cs                            | This defines the mapping between my 2 classes and the database tables. I am using 2 tables. |
| AktienKursAbfrager.cs                        | This fetches stock names and prices from the external website.                              |
| CustomRouteHandlerBuilder.cs                 | This is a module I found on Google so that I can use validation attributes for MinimalAPI.  |
| DataValidator.cs                             | See above.                                                                                  |
| _Jwt_                                        | Code proposed by Google and AI to cover the generation and signing of JWT tokens.           |
| MatchPropertyAttribute                       | Custom attribute to support regular expressions (proposed by AI).                           |
| Folder migrations                            | This is automatically generated by EF.                                                      |
| Benutzer.cs                                  | My model class to describe what a user is.                                                  |
| Transaktion.cs                               | Same approach for the transactions of stocks.                                               |
| \*Request.cs                                 | Classes to describe the format of incoming request to the API.                              |
| *Response.cs, *Result.cs PortfolioEintrag.cs | Classes to describe the format of outgoing responses from the API.                          |
| Program.cs                                   | Main class that starts the webserver and registers all routes that the API will respond to. |
| docker-compose.yml                           | Proposed by AI, this sets up a docker container to run PostgreSQL.                          |

## Detailed description Frontend

The client was generated with Angular CLI as template. I will now describe all files, that I added or modified significantly.

| File                | Description                                                                                                                                                                                                                                                          |
| ------------------- | -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| styles.scss         | For some global styles to indicated valid/invalid form fields.                                                                                                                                                                                                       |
| api.service.ts      | This has all the HTTP GET and POST requests to the routes from the backend. It also remembers if the user is logged in and stores/receives the JWT token to/from the Browser LocalStorage.                                                                           |
| app.routes.ts       | This defines all routes that the user can navigate to and which component should be used for it. E.g. when the user goes to the /sell route, the SellComponent will be shown.                                                                                        |
| auth-guard-service.guard.ts | This checks if a user is currently logged in (by getting this information from the api service). If not, it reroutes to /login. The purpose of this is to prevent user from accessing any other routes than /register and /login when they are not logged in. |
| Register Component  | Renders a form to create a new user. Does all the validation and then submits the request. It reroutes to login on success.                                                                                                                                          |
| Login Component     | Equivalent to register, reroutes to portfolio.                                                                                                                                                                                                                       |
| Quote component     | Lets the user enter a symbol and submit a quote request. Will display an error if there is an error server response, otherwise will display the price and is ready for the next request.                                                                             |
| Buy Component       | This includes the buy form where the user can input a symbol and quantity, it validates the inputs before the user can submit the buy request. It reroutes to the portfolio page in case of success or shows an error message, if the server responds with an error. |
| Sell Component      | It fetches the symbols of the stocks in the users posession upon load and populates the select input with it. Then it works equivalent to the buy component.                                                                                                         |
| History Component   | On load, it fetches all transactions from the API and displays them as table.                                                                                                                                                                                        |
| Portfolio Component | On load, it fetches all open positions aggregated from the transactions and the amount of cash from the server, calculates the total values and displays the data in a table. It also allows to directly buy more or sell the stocks for each line of the table.     |
| Header Component    | It displays the navbar and it checks if the user is logged in or not to render only the relevant links.                                                                                                                                                              |
| Footer Component    | It just displays a static information about the data source of the stock prices.                                                                                                                                                                                     |
