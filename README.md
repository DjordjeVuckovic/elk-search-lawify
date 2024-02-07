# Elastic Search for Serbian Laws and Contracts

## About the Project

This project aims to efficiently search through Serbian contracts and laws, utilizing the power of Elasticsearch to provide quick, relevant, and precise search results. It is designed to make legal documents easily accessible and searchable, streamlining the process of finding specific laws and contracts based on various search criteria.

## System Architecture

The system employs a microservices architecture for the backend, developed with .NET, and a dynamic, user-friendly frontend created using React. This approach ensures scalability, maintainability, and efficiency, capable of handling complex queries and delivering results swiftly.

The backend manages the logic for interfacing with the Elasticsearch , processing search queries, and data management. It acts as the project's foundation, ensuring all search functionalities are executed accurately.

Frontend has been developed with React, provides an intuitive interface for users. It enables users to input their search criteria, displays results in a user-friendly manner, and offers various filters to refine searches.

### System Architecture Diagram

Below is a diagram that illustrates the overall architecture of the system, including how the frontend, backend, and Elasticsearch interact:

![System Diagram](/docs/arhitecture.drawio.png)

For more detailed view, please open /docs/arhitecture.drawio diagram.
### Getting Started
If you are using docker, you can start the project by running the following command in the root directory:
```bash 
docker-compose up -d
```
Otherwise, you can start the project by running the following commands in the root directory:
```bash 
cd src
```
For each microservice, run the following command:
```bash 
dotnet run
```
To start the frontend, position yourself to SearchUI  and run the following command:
```bash 
 npm run dev
```
### Seed data
For laws you can use every serbian document that is available on internet in pdf format.
For contracts you need to use pdf documents defined in ```document-examples``` folder.
There is template of the contract so you can make your own contract and use it for searching.
### Technologies
- .NET 8
- React
- Elasticsearch
- Kibana
- Logstash
- Docker
- Nginx
- RabbitMQ
- Serilog
- Elastic.Clients.Elasticsearch
- ... and many more
### Application ui pictures

![System Diagram](/static/images/geospatial.png)
* geospatial search

![System Diagram](/static/images/geospatial-radius.png)
* geospatial search with radius

![System Diagram](/static/images/laws.png)
* laws full text search

![System Diagram](/static/images/law-info.png)
* law data nad metadata view

![System Diagram](/static/images/contracts.png)
* search contract by field

![System Diagram](/static/images/contracts-bool.png)
* bool search contract

### About the purpose of the project
This project built for the subject ```Upravljanje digitalnim dokumentima``` on the Faculty of Technical Sciences(FTN) in Novi Sad.
