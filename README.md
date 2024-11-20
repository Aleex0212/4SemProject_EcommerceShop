# 4SemProject_EcommerceShop : 
## **TO-DO**

- Tilføj Claims/Roles på User. Admin kan Put,Get,Delete.
- Ryde op i koden. Kommentatere på Api'er og Interfaces kun.  
---
**Done:**

- ✅ Lav `ProductService` om, så den følger REST niveau 2
- ✅ Implementer som et Dapr-workflow
- ✅ Flyt ansvaret for `OrderOrchestrator` til `OrderService`
- ✅ Få services til at kommunikere gennem Dapr
- ✅ Implementer Resiliance i OrderSerivce Persisantancelayer. bliver håndteret af ServiceDefault, fra Aspire.
- ✅ Lave Activities/Compensating Activities i OrderSerivce. Alle returnere bare (OK).
- ✅ Lave CreateOrder Workflow.
- ✅ DDD på `OrderService`
- ✅ Tilføj NotificationActivity 
- ✅ Tilføj JWT-tokens til API-gatewayen.
- ✅ Tilføj UserTypes.
- ✅ Tilføj policies på gateway.
- ✅ Tilføj Hasing af passwords på User. og input validering
  
---

## DAGBOG

- **31.10:** Forventningsafstemning, opbygning af arkitektur til projektet og arbejde med Domain-Driven Design. Enighed om services og projektets scope.
- **02.11:** Udfordringer: Hvordan implementerer man en Saga i microservices? Hvad er en Saga?
- **03.11:** Udfordringer: Forskel på `DaprClient` og `DaprWorkflowClient`? Hvordan får man services til at kommunikere? Overvejelser om RabbitMQ og Redis. Arbejde med `CreateOrderDaprWorkflow` og flytte Saga til `OrderService` som et workflow.
- **04.11:** Udfordringer: Debug i Dapr??
- **05.11:** Udfordringer: Dapr virker!!
- **06.11:** Vi Forventningsafstemmer igen. Projektet skalreres lidt ned med Domain-Driven-Design, så vi kan holde et fokus på hvad der er vigitgt.
- **07.11:** Workflow virker ikke. Actor fejl.
- **09.11:** Dapr workflow virker! Vi laver Refaktorering på koden og skalere ned. Nu virker det.
- **11.11:** Udfordringer : Arbejder med WorkFlows. Hvordan returnere vi result?  - problem løst senere på aftenen -
- **12.11:** tilføjelse af update og delete funkition på `OrderService.Api` Udfordringer: hvordan får jeg så den order med igennem?
- **13.11:** tilføjelse af en Simpel Database, Ordre Put,Post,Delete virker nu. Tilføjet NoificationActivity, udfordringer: Den kalder 2 gange?. 
- **16.11:** Tilføjelse af JWT implementering på Gateway Api'en. Udfordring: Hvorfor er vores Beere token ikke valid?? Løst i dag.
- **18.11:** Man kan nu lave claims baseret på hvilken usertype man logger ind med samt policies til at sikre det med.
- **20.11:** read på orders og tilføjelse af Hashing på Passwords og Inputvalidering.
