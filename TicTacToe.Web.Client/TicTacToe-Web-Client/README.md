# TicTacToeWebClient

Проверка версий системных библиотек
ng v

Проверка установленной локально версии Angular и CLI
npm list -global --depth 0

Сборка Docker-контейнера:
	docker build -t tictactoewebclient1.0 .

Запуск Docker-контейнера:
	docker run -d --name tictactoe_web_client -p 80:80 tictactoewebclient1.0