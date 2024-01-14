Hi there!

In this small project, I will be exploring the capabilites of **NATS**


Read about it here - https://nats.io/


TODOs in this project:
1. Simulate pub-sub of Nats core with subjects/pubs/subs.
2. Fan out model with single publisher for two/three subs
3. Implement a queue group with load balancing.
4. Maybe move to JetStream to try streaming, etc.
5. Maybe connect another node app written in nestjs as clients to check TS clients.


Setup: 
1. Starting this with .net 7 background service as a publisher.
2. To run locally on machine, using choco, installed a executable of nats server locally. Simply `$  choco install nats-server`
3. Update on docker support: Using docker-compose, a nats image is used instead of pt 2. The URL used in connection is pointed to this service by default.
