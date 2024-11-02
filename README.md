4SemProject_EcommerceShop
Projekt TO-DO

- Implementer GatewayApi Rigtigt
- Lav Activities og Komensating activities på CreateOrderWorkflow
- Tilføj JWT-Tokens til API-Gateway
- Tilføj Database.EnsureCreated på DBContext
- Tilføj IdentityId på CustomerId - Identity håndteres på Microservice
___________________________________________________________
(OK)-  Lav productservice om til at den følger rest niveau 2
(OK)-  Implementer som et Dapr-WorkFlow 
(OK) - Flyt ansvaret for OrdreOrhestrator til Ordre Service
(OK) - Få Services til at komminikere gennem Dapr
___________________________________________________________
DAGBOG: 
31.10 : Forventningsafstem, lave arkitetturen til projektet, Domain-Driven-Design. enighed om Services og Projekt Scope.
02.10 : Udfordringer : SAGA hvordan implementerer man en saga i microservices? Hvad er en SAGA.
03.10 : Udfordringer : DaprClient/DaprWorkFlowClient ?? Hvorfor er de forskellige? Hvordan for man services til at snakke sammen? 
        RabbitMq? Redis? Lave Dapr Workflow, CreateOrderDaprWorkflow. og flytte SAGA til OrderService - Implementer som et workflow.
04.10 : Arbejde med Activities og Kompenstating Transactions.
