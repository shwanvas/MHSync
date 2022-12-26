# MH Data SYNC #
## Project Description ##
    The client would like to develop an independent application Message Bus in order to automate the process of 
    syncing data between users belongs to various vertical. It follows the BFF pattern. BFF can provide a 
    client-specific backend mediator and act as a proxy that forwards and merges multiple requests to different 
    service APIs.
## Enivronment
* Visual Studio 2022
  * .Net 6 web Api
  * swagger  
*  Apache Kafka
* Angular 15
* SqlServer

* Name of the files associated
  * Apis
    * ApacheKafkaInsertService
    * ApacheKafkaUpdateService
    * MHSync
    * MHUpdateSync
  * Angular UI
    * MH_sync_ui
    * MH_update_sync
## Initialization
To initialize after clonning
You should Apache Kafka and Sql server
In Apache Start the Zookeeper and Kafka server by following command

        zookeeeper-server-start ../../config/zookeeper.properties
        kafka-server-start ../../config/server.properties

To Connect with sqlserver use your connection strings in the appsettings.json file of each dotnet application.

For each Dotnet application Go inside project folder and Open the project using the solution file to view in Visual Studio 2022 else Go to the Specific folder open command Prompt and run command to run.

      dotnet run

For each Angular application run using
    
    ng serve -o
    
Running Method

* Apache Zookeeeper and Kafka server
* Then Dotnet Application
* and Angular application

