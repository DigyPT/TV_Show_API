# TV_Show_API
How to use:

Infelizmente esta API não está disponivel online , é apenas para uso local. Por este motivo , é necessário fazer download do projecto e iniciar o projecto.
É recomendado utilizar ou swagger ou Postman para o teste desta API.

Ao inicar o projecto,  página do seu browser predefinido irá aparecer com um layout em swagger, este layout tem o propósito de testar a API. 
No endereço do site poderá ver algo do género "https://localhost:44355/swagger/index.html".
Só irá precisar do excerto "https://localhost:44355/" e adicionar "api/".

Primeiro que tudo , irá precisar de se registar usando o comando POST do url "https://localhost:44355/api/user/register" e enviar o seu username, password e e-mail
(A password será encriptada e guardada na Base de Dados local do projecto)

Segundo , será necessário fazer o login a partir do URL "https://localhost:44355/api/user/login?username=[username]?password=[password]". 
Será feita uma verificação dos dados enviados, e caso haja registo deles será devolvida o seu token (é importante guardar este token,
será necessário para efectuar o resto das pesquias)

Depois de ter sido feito o login pode utilizar os seguintes métodos/funções;

[Metodos GET] (Substituir todos os [] com os dados a enviar)
[SERIE]
Receber todas as séries:
>"https://localhost:44355/api/series/?token=[token]" ou "https://localhost:44355/api/series/?page=[pagenumber]&pagesize=[pagesize]&token=[token]" (se não enviar o número
da página (page) e/ou o tamanho dela (pagesize) a API irá retornar o valores default);

Receber mais detalhes de uma série:
>"https://localhost:44355/api/series/moredetails?serieid=[serieid]&token=[token]", irá devolver todos os detalhes da serie especificada incluido os seus episódios;

Receber todos os actores associados a uma série:
>"https://localhost:44355/api/series/seriesactors?serieid=[serieid]&token=[token]", irá devolver todos os atores associados há serie indicada;

Receber todas as séries associados a um género:
>"https://localhost:44355/api/series/seriegenre?genre=[genre]&token=[token]" irá devolver todas as séries associadas ao genéro associado;

[Actors]
Receber todos os actores:
>"https://localhost:44355/api/actores/?token=[token]", irá devolver todos actores na Base de Dados;

Receber todas as series a que um actor está associado:
>"https://localhost:44355/api/actores/getallseries?actorname=[actorname]&token=[token]", irá devolver todas as séries associadas a um actor;

[Episodes]
Receber todos os episódios:
>"https://localhost:44355/api/episodes/?token=[token]" , irá devolver todos os episodios;

[User]
Receber os detalhes do utilizador:
>"https://localhost:44355/api/user/getuserdetails?username=[username]&token=[token]", irá devolver todos os detalhes do utilizador, inclusive, as series favoritas;

[METODOS POST]
[User]
Adicionar serie aos favoritos:
>"https://localhost:44355/api/user/addfavserie?userid=[userid]&serieid=[serieid]&token=[token]", adiciona uma serie aos favoritos;

Remover serie dos favoritos:
>"https://localhost:44355/api/user/removefavserie?userid=[userid]&serieid=[serieid]&token=[token]",remove uma series dos favoritos;

[Actors]
Adicionar Actor
>"https://localhost:44355/api/actores/" , enviar um objecto do tipo Actor com actorname, date_of_birthday;

[Episodes]
Adicionar episodio
>"https://localhost:44355/api/episodes/", enviar um objecto do tipo Episode com episode_number, season, episode_name, air_date e SerieId; (Já tem que estar criado a serie associada) 

[Series]
Adicionar series
>"https://localhost:44355/api/series/":
Enviar > Objecto do tipo Serie com name,country,stard_date,end_date,network,status,rating, objecto do tipo ActorSeries com actorid e serie id (opcional) e um objecto
          genreSeries com genreid e serieid;
       

