kubectl apply -f ./postgres.yaml
kubectl apply -f ./identity-server.yaml

kubectl get all

read -p "Press any key to close this window" x