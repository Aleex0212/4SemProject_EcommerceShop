# 4SemProject_EcommerceShop : **TO-DO**

- Lave Activities/Compensating Activities i OrderSerivce. Alle returnere bare (OK).
- Lave CreateOrder Workflow.
- Ryde op i koden. Kommentatere på Api'er og Interfaces kun.
- Tilføj JWT-tokens til API-gatewayen.
- Implementer Resiliance i OrderSerivce Persisantancelayer.
  
---

**Done:**
- ✅ Lav `ProductService` om, så den følger REST niveau 2
- ✅ Implementer som et Dapr-workflow
- ✅ Flyt ansvaret for `OrderOrchestrator` til `OrderService`
- ✅ Få services til at kommunikere gennem Dapr

---

# DAGBOG:

- **31.10:** Forventningsafstemning, opbygning af arkitektur til projektet og arbejde med Domain-Driven Design. Enighed om services og projektets scope.
- **02.10:** Udfordringer: Hvordan implementerer man en Saga i microservices? Hvad er en Saga?
- **03.10:** Udfordringer: Forskel på `DaprClient` og `DaprWorkflowClient`? Hvordan får man services til at kommunikere? Overvejelser om RabbitMQ og Redis. Arbejde med `CreateOrderDaprWorkflow` og flytte Saga til `OrderService` som et workflow.
- **04.10:** Udfordringer: Debug i Dapr??
- **05.10:** Udfordringer: Dapr virker!!
- **06.10:** Vi Forventningsafstemmer igen. Projektet skalreres lidt ned med Domain-Driven-Design, så vi kan holde et fokus på hvad der er vigitgt.
