
kubectl delete service amina-postgres-service --ignore-not-found=true
kubectl delete service amina-is-service --ignore-not-found=true
kubectl delete deployment amina-is-deployment --ignore-not-found=true
kubectl delete deployment amina-postgres-deployment --ignore-not-found=true
kubectl delete ReplicaSet amina-is-deployment --ignore-not-found=true
kubectl delete pod amina-is-deployment --ignore-not-found=true

read -p "Press any key to close this window" x