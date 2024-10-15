export resource_group_name="djoufson-learnings"
export service_principal_name="gitlab-deployment"
# export subscription_id=$(az account show --query id --output tsv)

# Login using az-cli
az login
az account set --subscription $subscription_id


az ad sp create-for-rbac --name $service_principal_name --role contributor --scopes /subscriptions/$subscription_id/resourceGroups/$resource_group_name
