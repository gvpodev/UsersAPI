# UsersAPI

Esta � uma API para gerenciamento de usu�rios, com endpoints para autentica��o, recupera��o de senha e manipula��o de contas de usu�rio.

## Endpoints

### Autentica��o

**POST /api/auth/login**

Endpoint para autenticar o usu�rio.

**POST /api/auth/forgot-password**

Endpoint para recuperar a senha de acesso do usu�rio.

**POST /api/auth/reset-password**

Endpoint para reiniciar a senha de acesso do usu�rio.

### Usu�rios

**POST /api/users**

Endpoint para criar uma nova conta de usu�rio.

**PUT /api/users**

Endpoint para alterar os dados da conta do usu�rio.

**DELETE /api/users**

Endpoint para excluir a conta do usu�rio.

**GET /api/users**

Endpoint para consultar os dados da conta do usu�rio.
