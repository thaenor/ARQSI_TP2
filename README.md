ARQSI_TP2
=========
#NOTAS TEMPORÁRIAS PARA O DESENVOLVIMENTO#

##Criar a base de dados local da editora##
1. From the Tools menu, click Library Package Manager and then Package Manager Console
2. At the PM> prompt enter the following command:
> enable-migrations -contexttypename MusicContext

![It should show this](http://i2.asp.net/media/4336278/1pm2.png?cdn_id=2014-11-11-001)

3. > add-migration InitialCreate
update-database

![It should show this](http://i3.asp.net/media/4336302/1addMIg.png?cdn_id=2014-11-11-001)

you should be able to see this in the server explorer

![something](http://i1.asp.net/media/4336272/1dbG.PNG?cdn_id=2014-11-11-001)

=========
##Our progress so far##

2/12/2014:
We are using the code first method to implement the record label.
To make use of the default authentication system we are assuming the admin and manager data are hard coded into the DB with respective emails. When these emails are used to login they will be redirected to the respective page. Manager gets a full CRUD page for Albums while admin will get the same for Sales.

**Important questions**
 > how do we deal with the DB context and default authentication connection. And reroute them to the Gandalf server?

=========
##Project Overview##

This project is developed in the context of system architecture classes (ARQSI).
It consist in creating an "ecosystem" for music album stores. The project consists in a total of 3 different servers.

1. IDEIMusic: A record label. It creates music albums and supplies them to music stores. This is developped in ASP.Net

2. MusicStore: A music store that buys music albums from the label editor and resells them to the end users (costumers). This is developped in PHP **(using framework NuSOAP and MINI)**

3. ImportMusic: This server consist in a market analyst, it records sales and stores them in it's own database.

**Important notes:**

. each server has it's own database
. for the purposes of this project MusicStore's database will consist in a part of IDEIMusic's DB, *which will be the albums purchased from the label editor*

=========

###Documentation###
####Data model - Modelo de dados####

![arqsi_tp2_dbmodel](https://cloud.githubusercontent.com/assets/3703930/5285514/8e801f52-7b14-11e4-8854-58d770f53360.png)

####logical view - Lógica de comunicação entre servidores####

![general model](https://cloud.githubusercontent.com/assets/3703930/5154396/64ba4636-7252-11e4-8a10-3c27a028a98b.png)

![register store sequence](https://cloud.githubusercontent.com/assets/3703930/5154397/64ba7b60-7252-11e4-8361-2de8a8efe12a.png)

=========

####Comunicação IDEIMusic - MusicStore####

A comunicação é realizada usando SOAP (XML).
O gestor da MusicStore (para o caso deste exercício, no geral será aplicado para todos os clientes da editora) deve estar registo e ser detentor de uma chave API *Application Programming Interface*.

Uma encomenda da editora para a loja de música é iniciada pela loja com um pedido inicial HTTP getAllAlbums().

A editora devolve uma lista em formato SOAP (XML) com todos os albuns disponiveis.

O gerente da MusicStore pode então preparar uma resposta com os discos que pretende encomendar (assim como a sua quantidade).

Em caso de sucesso, uma resposta da IDEIMusic confirma a encomenda. Caso contrário, são devolvidos os erros.

=========

##Resumo do projecto##

Este projecto foi desenvolvido no contexto da disciplina do ISEP de Arquitectura de Sistemas (ARQSI).
Consiste no desenvolvimento de um "ecossitema" de compra e venda de albuns musicas. Para tal serão implementados 3 diferentes servidores independentes que comunicam entre si.

1. IDEIMusic: Desenvolvida em ASP.Net, é a editora de albuns. Responsável pelo fornecimento dos mesmos às lojas de música no geral

2. MusicStore: Desenvolvida em PHP. Refere-se a uma loja de musica que recebe os albuns da editora IDEIMusic para posteriormente os redistribuir para os clientes finais.

3. ImportMusic: Um servidor de análise de mercados. As compras registadas são enviadas para este, e posteriormente registadas numa base de dados

**Notas importantes:**

. Cada servidor possuí a sua própria base de dados

Autores:
. Francisco Santos (1111315)

. Rui Silva (1121296)

. Sofia Sá (1100537)
