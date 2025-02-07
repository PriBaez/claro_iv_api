# 📄 Documentación API

## 🚀 Introducción

Este documento proporciona una descripción general de la arquitectura de la API, incluida su capa de servicio, capa de repositorio y modelos de datos. Su objetivo es ayudar a los desarrolladores a comprender la estructura y la funcionalidad de la API.

Base URL:

```
http://localhost:5290/api
```

## Modelos de datos - (Nombre del endpoint)

- Producto (Product)
- Categoria (Category)
- Variante de producto (Variant)

### DTO (Data Transfer Objects)

- Vista de producto (ProductView)

## 📦 Endpoints Genericos

### 1️⃣ Get All

- **URL:** `/{controller}`
- **Método:** `GET`
- **Descripción:** Retorna una lista del modelo de datos referidos en ese controlador.

**Request de ejemplo:**

```bash
curl -H http://localhost:5290/api/Product
```

**Respuesta:**

```json
[
  [
    {
      "id": 1,
      "name": "Camiseta de algodón",
      "categoryId": 1,
      "priceRange": "900.00-1200.00",
      "colors": ["azul", "rojo"],
      "category": null,
      "productVariants": [
        {
          "id": 1,
          "productId": 1,
          "color": "azul",
          "stock": 12,
          "price": 1200,
          "product": {
            "id": 1,
            "name": "Camiseta de algodón",
            "categoryId": 1,
            "category": null,
            "productVariants": null
          }
        },
        {
          "id": 2,
          "productId": 1,
          "color": "rojo",
          "stock": 20,
          "price": 900,
          "product": {
            "id": 1,
            "name": "Camiseta de algodón",
            "categoryId": 1,
            "category": null,
            "productVariants": null
          }
        }
      ]
    }
  ]
]
```

### 2️⃣ Get element by ID

- **URL:** `api/{controller}/{id}`
- **Método:** `GET`
- **Descripción:** Retorna una entidad especifica por ID.
- **Path Parameter:** `id` (requerido): el ID de la entidad a buscar.

**Request de Ejemplo:**

```bash
curl -H http://localhost:5290/api/Product/1
```

**Respuesta:**

```json
{
  "id": 1,
  "name": "Camiseta de algodón",
  "categoryId": 1,
  "category": null,
  "productVariants": []
}
```

### 3️⃣ Create a New element

- **URL:** `/{controller}`
- **Método:** `POST`
- **Descripción:** Crea un nuevo elemento de la entidad especificada.
- **Request de Ejemplo Body:**
  ```json
  {
    "id": 0,
    "name": "string",
    "categoryId": 0,
    "isDeleted": true,
    "category": {
      "id": 0,
      "name": "string",
      "isDeleted": true,
      "products": ["string"]
    },
    "productVariants": [
      {
        "id": 0,
        "productId": 0,
        "color": "string",
        "stock": 0,
        "price": 0,
        "product": "string"
      }
    ]
  }
  ```

**Request de Ejemplo:**

```bash
curl -X POST -H "Content-Type: application/json" -d '{
  "id": 0,
  "name": "string",
  "categoryId": 0,
  "isDeleted": true,
  "category": {
    "id": 0,
    "name": "string",
    "isDeleted": true,
    "products": [
      "string"
    ]
  },
  "productVariants": [
    {
      "id": 0,
      "productId": 0,
      "color": "string",
      "stock": 0,
      "price": 0,
      "product": "string"
    }
  ]
}' http://localhost:5290/api/Product/1
```

**Respuesta:**

```json
Producto creado correctamente
```

### 4️⃣ Update a element

- **URL:** `/{controller}/{id}`
- **Método:** `PUT`
- **Descripción:** Actualiza un elemento de una entidad existente
- **Path Parameter:** `id` (requerido)
- **Request Body:**
  ```json
  {
    "id": 0,
    "name": "string",
    "categoryId": 0,
    "isDeleted": true,
    "category": {
      "id": 0,
      "name": "string",
      "isDeleted": true,
      "products": ["string"]
    },
    "productVariants": [
      {
        "id": 0,
        "productId": 0,
        "color": "string",
        "stock": 0,
        "price": 0,
        "product": "string"
      }
    ]
  }
  ```

**Request de Ejemplo:**

```bash
curl -X PUT -H "Content-Type: application/json" -d '{
  "id": 0,
  "name": "string",
  "categoryId": 0,
  "isDeleted": true,
  "category": {
    "id": 0,
    "name": "string",
    "isDeleted": true,
    "products": [
      "string"
    ]
  },
  "productVariants": [
    {
      "id": 0,
      "productId": 0,
      "color": "string",
      "stock": 0,
      "price": 0,
      "product": "string"
    }
  ]
}' http://localhost:5290/api/Product/1
```

**Respuesta:**

```
204 - No content
```

### 5️⃣ Delete a element

- **URL:** `/{controller}/{id}`
- **Método:** `DELETE`
- **Descripción:** Elimina un elemento de una entidad existente
- **Path Parameter:** `id` (requerido)

**Request de Ejemplo:**

```bash
curl -X DELETE http://localhost:5290/api/Product/1
```

**Respuesta:**

```
204 No Content
```

## ⚠️ Manejo de errores
Al ocurrir un error el API devolvera un mensaje que puede ser de ayuda para el desarrollador (dependera de el que se le muestra y que no al usuario). Estos son los codigos mas comunes

- **200 OK:** Tu solicitud se proceso con exito (puede venir con data de retorno o No).
- **204 No Content:** Tu solicitud fue procesada con exito pero no se suele devolver nada aqui.
- **400 Bad Request:** entrada de datos invalida.
- **500 Internal Server Error:** Algo salió mal en el servidor.

## 💡 Notas

- Para correr la API solo tienes que abrir tu linea de comandos y ejecutar `dotnet run` o usar Visual Studio y ejecutar F5.
- Todas las respuestas son en formato JSON.


## Descripción general de la arquitectura

### Arquitectura en capas

La API se creó utilizando una arquitectura en capas que promueve la separación de preocupaciones, la escalabilidad y la capacidad de mantenimiento.

- **Capa del controlador:** maneja las solicitudes y respuestas HTTP. Actúa como un punto de entrada para la API.
- **Capa de servicio:** contiene la lógica empresarial y organiza el flujo de datos entre los controladores y los repositorios.
- **Capa del repositorio:** administra la persistencia de los datos, interactuando con la base de datos.
- **Capa del modelo:** define la estructura de los datos utilizando objetos de transferencia de datos (DTO) y modelos de entidad.

### 2.2 Flujo de solicitud

1. **Solicitud del cliente:** un cliente envía una solicitud HTTP a la API.
2. **Controlador:** La solicitud es recibida por un controlador, que valida la entrada y delega el procesamiento a la capa de servicio.
3. **Servicio:** La capa de servicio procesa la solicitud, aplicando lógica empresarial y se comunica con los repositorios para recuperar o almacenar datos.
4. **Repositorio:** El repositorio interactúa con la base de datos para realizar operaciones CRUD.
5. **Respuesta:** Los datos se devuelven al servicio, luego al controlador y, finalmente, se envían de vuelta al cliente.

## Componentes

### Controladores

- Manejar puntos finales de API.
- Validar solicitudes.
- Devolver respuestas HTTP estandarizadas.

### Servicios

- Implementar lógica empresarial central.
- Manejar la transformación de datos entre controladores y repositorios.
- Gestionar transacciones si es necesario.

### Repositorios

- Abstraer operaciones de base de datos.
- Proporcionar una API limpia para el acceso a los datos.

### Modelos

- **DTO:** Se utilizan para transferir datos entre capas.
- **Entidades:** Representan tablas de bases de datos.

## Ejemplo

```texto sin formato
Request del cliente (POST /api/products)
   ↳ ProductController (valida el request)
       ↳ ProductService (aplica logica de negocio)
           ↳ ProductRepository (interactua con la base de datos)
               ↳ Base de datos
           ↲ Data es retornada al servicio
       ↲ Respuesta formateada para el controlador
   ↲ Respuesta enviada al cliente
```

## 5. Tecnologías utilizadas

- **Marco de backend:** .NET
- **Base de datos:** SQL Server
- **Frontend:** React

## 6. Notas adicionales

- Utilización de inyección de dependencias para la gestión de servicios y repositorios.
- Uso de servicios genericos para las entidades que no harian mas que las acciones básicas.
- Implementación el manejo y registro de errores en cada capa para una mejor capacidad de mantenimiento.

