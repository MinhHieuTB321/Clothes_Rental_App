"# Clothes_Rental_App" 
# Config docker

---------------- Create Combo Image -----------------
docker build -f .\ComboService.WebApi\Dockerfile -t [docker-name]/comboservice .
docker push [docker-name]/comboservice

---------------- Create User Image -----------------
docker build -f .\UserService.WebApi\Dockerfile -t [docker-name]/userservice .
docker push [docker-name]/userservice

---------------- Create Order Image -----------------
docker build -f .\OrderService.WebApi\Dockerfile -t [docker-name]/orderservice .
docker push [docker-name]/orderservice

---------------- Create Shop Image -----------------
docker build -f .\ShopService.WebApi\Dockerfile -t [docker-name]/shopservice .
docker push [docker-name]/shopservice

---------------- Create Sql Server -----------------
kubectl apply -f local-pvc.yaml          
kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55w0rd!"
kubectl apply -f mssql-depl.yaml

---------------- Create Ingress -----------------
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.8.2/deploy/static/provider/cloud/deploy.yaml
kubectl apply -f ingress-srv.yaml    


---------------- Create RabbitMQ Host -----------------
kubectl apply -f rabbitmq-depl.yaml

---------------- Create Deployment -----------------
kubectl apply -f users-depl.yaml

kubectl apply -f shops-depl.yaml

kubectl apply -f orders-depl.yaml

kubectl apply -f combos-depl.yaml


---------------- Base Command -----------------
kubectl get deployment
kubectl get pods
kubectl get services

---------------- Delete Deployment -----------------
 kubectl delete deployment [Name - Deployment]
