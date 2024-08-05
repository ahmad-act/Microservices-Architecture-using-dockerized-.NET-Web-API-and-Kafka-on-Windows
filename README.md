# Microservices Architecture using dockerized .NET Web API and Kafka on Windows

## Microcervice concept

### Apply loosely coupled relationship between Book Information and Book Reservation Services.

![Microcervice concept](https://raw.githubusercontent.com/ahmad-act/public-resources/main/Microservice/Microservices%20Architecture%20Using%20.NET%20Web%20API%20and%20Kafka%20on%20Windows.jpg?raw=true)

##### You have set up a microservices architecture using .NET Web API and Kafka on Windows. This setup involves two services, Book Information and Book Reservation, communicating through Kafka in a loosely coupled manner. Docker Desktop is used to manage the Kafka container, and each service is run locally for development and testing purposes.

## Prerequisites

1. Docker Desktop: Download and install Docker Desktop from [Docker's official website](https://www.docker.com/products/docker-desktop/).

2. .NET SDK: Download and install the .NET SDK from [Microsoft's official website](https://dotnet.microsoft.com/download).

3. Application: Clone the project on your computer:
 
    **Using the TortoiseGit or GitBash or PowerShell:**
    
    ```sh
    git Clone 
    ```

4. Kafka Setup: You will need the setup Kafka to use in application.

## Step 1: Set up Kafka using Docker

1. Go to the Kafka Docker Compose file location:

    ```sh
    cd "Microservices-Architecture-using-dockerized-.NET-Web-API-and-Kafka-on-Windows"
    ```

2. Run the Kafka Container:

    ```sh
    docker-compose -f install-kafka.yml up -d
    ```

3. Access Kafka Management UI:

    **Open your browser and navigate to http://localhost:8080.**

## Step 2: Build and Run the Application

 1. Build and Run the Book Information Services:
 
    **Execute the following commands using PowerShell:**
    
    ```sh
    cd "Microservices Architecture using dockerized .NET Web API and Kafka on Windows\BookInformationService\BookInformationService"

    docker build --build-arg ENVIRONMENT=Development -t engzaman2020/book-information-api-v1-image:1 .

    docker run -it -p 3101:3101 --name book-information-api-v1-container engzaman2020/book-information-api-v1-image:1
    ```
 
    **Use the http://localhost:3101/swagger/index.html**
 
 2. Build and Run the Book Reservation Services:
 
    **Execute the following commands using PowerShell:**
    
    ```sh
    cd "Microservices Architecture using dockerized .NET Web API and Kafka on Windows\BookReservationService\BookReservationService"

    docker build --build-arg ENVIRONMENT=Development -t engzaman2020/book-reservation-api-v1-image:1 .

    docker run -it -p 3102:3102 --name book-reservation-api-v1-container engzaman2020/book-reservation-api-v1-image:1
    ```
    
    **Use the http://localhost:3102/swagger/index.html**
