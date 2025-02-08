# **Lån Applikation API Dokumentation**

### Översikt

Uppgiften utgick ifrån att skapa en lån applikation som utgår ifrån en API. Den ska tillåta användare att hantera låneansökningar med hjälp av API:et CRUD operationer, såsom skapande, uppdatering, hämtning och radering av låneansökningar.



#### URL

API:et var tillgängligt via en URL: https://localhost:7161/api/loanapplications, samt även https://localhost:7161/api/loanapplications/status/all för att få fram statusen för alla låneansökningars lån status.



#### Endpoint

De endpoint jag använde mig av var:

* Metod: 'GET'

* Beskrivning: Hämtar en lista av alla låneansökningar

* Exempel på Respons:

  [
      {
          "id": 1,
          "borrower": "Namn Efternamn",
          "amount": 10000,
          "status": "Submitted",
          "date": "2024-01-23"
      },
      ...
  ]

* Metod: 'GET'

* Beskrivning: Hämtar en specifik låneansökan baserat på ID

* Exempel på Respons:

  {
      "id": 2,
      "borrower": "Namn Efternamn",
      "amount": 5000,
      "status": "Approved",
      "date": "2024-01-24"
  }

* Metod: 'POST'

* Beskrivning: Skapar en ny låneansökan

* Exempel på Request:

  {
      "borrower": "Namn Efternamn",
      "amount": 15000,
      "status": "Submitted"
  }

* Exempel på Respons:

  {
      "id": 3,
      "borrower": "Namn Efternamn",
      "amount": 15000,
      "status": "Submitted",
      "date": "2024-01-25"
  }

* Metod: 'PUT'

* Beskrivning: Uppdaterar en befintlig låneansökan, samt att man använder ID för lånesökaren som ska uppdateras.

* Exempel på Request:

  {
      "amount": 20000,
      "status": "Approved"
  }

* Metod: 'DELETE'

* Beskrivning: Raderar en specifik låneansökan baserat på ID

* Exempel på Respons: '204 No Content' kommer det stå.



* Om låneansökan inte finns så får man fram ett sån meddelande: 

  "errors": [

  ​      {

  ​        "exception": **null**,

  ​        "errorMessage": "Loan application does not exist."

  ​      }

  ​    ]

  



### Detaljerad Endpoint

 Endpoint i `LoanApplicationsController`

#### 1. Hämta Alla Låneansökningar

- **Endpoint:** `GET /api/loanapplications/`
- **Funktion:** `GetAll()`
- **Beskrivning:** Returnerar alla låneansökningar.
- **Respons:** En lista av `LoanApplication`-objekt.

#### 2. Hämta Låneansökan med ID

- **Endpoint:** `GET /api/loanapplications/{id}`
- **Funktion:** `GetById(int id)`
- **Beskrivning:** Returnerar en specifik låneansökan baserat på ID.
- **Respons:** Ett `LoanApplication`-objekt om det finns, annars `NotFound`.

#### 3. Skapa Ny Låneansökan

- **Endpoint:** `POST /api/loanapplications/`
- **Funktion:** `Create([FromBody] LoanApplication application)`
- **Beskrivning:** Skapar en ny låneansökan.
- **Request:** Ett `LoanApplication`-objekt.
- **Respons:** `CreatedAtAction` med det skapade objektet, eller `BadRequest` om en liknande ansökan redan finns.

#### 4. Uppdatera Låneansökan

- **Endpoint:** `PUT /api/loanapplications/{id}`
- **Funktion:** `Update(int id, [FromBody] LoanApplication application)`
- **Beskrivning:** Uppdaterar en befintlig låneansökan.
- **Request:** Ett `LoanApplication`-objekt.
- **Respons:** `NoContent` om uppdateringen lyckades, `BadRequest` om ID inte stämmer, eller `NotFound` om ansökan inte finns.

#### 5. Radera Låneansökan

- **Endpoint:** `DELETE /api/loanapplications/{id}`
- **Funktion:** `Delete(int id)`
- **Beskrivning:** Raderar en specifik låneansökan baserat på ID.
- **Respons:** `NoContent` om raderingen lyckades, annars `NotFound`.