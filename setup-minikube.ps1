# setup-minikube.ps1

Write-Host "[INFO] Configurando ambiente Docker do Minikube..." -ForegroundColor Cyan
$envVars = & minikube docker-env --shell powershell | Out-String
Invoke-Expression $envVars

Write-Host "[INFO] Ambiente Docker configurado no Minikube!" -ForegroundColor Green

# Build da imagem da API diretamente no Docker do Minikube
Write-Host "[INFO] Buildando imagem da API dentro do Minikube..." -ForegroundColor Cyan
docker build -t api:latest -f API/Dockerfile .

Write-Host "[INFO] Build concluído!" -ForegroundColor Green

# Aplicar todos os manifests do diretório k8s/
Write-Host "[INFO] Aplicando manifests Kubernetes..." -ForegroundColor Cyan
kubectl apply -f k8s/

# Restart para forçar nova versão da imagem
Write-Host "[INFO] Reiniciando deployment da API..." -ForegroundColor Cyan
kubectl rollout restart deployment api-deployment

# Espera o pod ficar pronto
Write-Host "[INFO] Aguardando pods ficarem prontos..." -ForegroundColor Yellow
kubectl wait --for=condition=available --timeout=120s deployment/api-deployment

# Inicia o port-forward na porta 8080
Write-Host "[INFO] Iniciando port-forward na porta 8080..." -ForegroundColor Cyan
Start-Process powershell -ArgumentList "kubectl port-forward service/api-service 8080:8080" -NoNewWindow

# Abre o navegador automaticamente
Start-Sleep -Seconds 3
Write-Host "[INFO] Abrindo o navegador no Swagger..." -ForegroundColor Green
Start-Process "http://localhost:8080/swagger"

Write-Host "[SUCCESS] Ambiente pronto em http://localhost:8080/swagger" -ForegroundColor Green
