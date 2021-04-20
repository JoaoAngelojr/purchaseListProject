# Purchase Lists Project

A Web API for calculate bills of Purchase Lists. The description of the challenge is found [here](https://gist.github.com/programa-elixir/1bd50a6d97909f2daa5809c7bb5b9a8a).

## Getting Started

This project was developed using .NET Core 3.1, mainly following mediator design pattern. 
These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

You will need to have installed:

```
.NET Core SDK
```

### Running the solution

After cloning the project, open it in Visual Studio IDE then set PurchaseList.API as default startup project.
You can run the project by clicking in the 'Start' option (IIS Express), pressing F5 (for debugging mode) or Ctrl + F5 (without debugging mode).

The main Swagger page will be shown and then you can make requests clicking in 'CalculateBills' method then in 'try it out' button.
After that, create the request body. There's a sample request for you in the method explanation, but feel free to create your own request.

### Running unit tests

There's a XUnit project in the solution called PurchaseList.Tests where Controllers, RequestHandlers and Validators are tested.
For running all the unit tests, when you're using Visual Studio IDE, you'll need just to press Ctrl+R,A.
If you want to see the Test Explorer screen for debbuging, see all tests or anything else, just click in 'Tests' tab then click in 'Test Explorer' option.
