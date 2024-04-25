# Number to text converter

Here are some decisions I've made based on requirements.

Input examples include spaces, which means that we should use the text input.
Also number input is culture-dependent, and would use dot as a decimal separator in, for example, en-US.

Keeping this in mind, I've decided to keep API more strict and accept only numbers as a request parameter - making it a client's responsibility to transform user input into a `.` separated number. I wasn't sure about how restrictive the input validation should be, so " 123  456" is considered a valid input of 123456 and "123," is considered a valid input of 123,00. It's not perfect, of course.

To try the application you can dowload the zip from releases - it includes both API and client, so just unpack, run `API.exe` and go to http://localhost:5000 (or whatever the port would be). The api endpoint is available at `/api/convertion/{value}` (for example http://localhost:5000/api/convertion/1.34).

The published executable is framework-dependent and requires .NET 8

Of course you can get the sources and:
- `dotnet run --project Server/API` to run backend at http://localhost:5195
- `cd Client`, `npm install` and `npm start` to start the client at http://localhost:4200.

In development mode client will sent requests to hardcoded http://localhost:5195, so everything should be up and running. Sorry for not packing everything into a single container.

I've also skipped https, added CORS and didn't care about exception handling middleware for API.