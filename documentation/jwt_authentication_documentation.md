# 🔐 JWT Authentication Documentation (Spring Boot)

## ✨ What is JWT?
**JWT (JSON Web Token)** is a compact, self-contained way to securely transmit authentication information between a client and a server.

A JWT has three parts:
- **Header** → token type and algorithm (e.g., HS256)
- **Payload** → claims (userId, roles, expiration, etc.)
- **Signature** → ensures integrity and prevents tampering

JWTs are **stateless**, meaning the server does not store session data. Every request carries its own proof of authentication.

---

## 🧠 Authentication Flow

1. **Login**
   - Client sends credentials
   - Server validates them
   - Server returns an **Access Token** (and optionally a Refresh Token)

2. **Access Protected Resources**
   - Client sends the token in the request header:
     `Authorization: Bearer <JWT>`
   - Server validates the token and authorizes the request

---

## 🧩 Core Components

### JWT Service
Responsible for:
- Generating tokens
- Validating tokens
- Extracting claims

### Authentication Filter
- Intercepts incoming requests
- Reads the Authorization header
- Validates the token
- Sets the security context

### Security Configuration
- Defines public vs protected endpoints
- Registers the JWT filter

---

## 📦 Maven Dependencies (JJWT)

```xml
<dependency>
  <groupId>io.jsonwebtoken</groupId>
  <artifactId>jjwt-api</artifactId>
  <version>0.12.5</version>
</dependency>
<dependency>
  <groupId>io.jsonwebtoken</groupId>
  <artifactId>jjwt-impl</artifactId>
  <version>0.12.5</version>
  <scope>runtime</scope>
</dependency>
<dependency>
  <groupId>io.jsonwebtoken</groupId>
  <artifactId>jjwt-jackson</artifactId>
  <version>0.12.5</version>
  <scope>runtime</scope>
</dependency>
```

---

## ⚙️ Configuration (application.yml)

```yml
security:
  jwt:
    secret: "CHANGE_ME_TO_A_LONG_RANDOM_SECRET"
    accessTokenMinutes: 15
    issuer: "your-application-name"
```

---

## 🏗️ JWT Service Example

```java
public class JwtService {
    // Token generation, validation, and claim extraction logic
}
```

---

## 🛡️ JWT Authentication Filter

```java
public class JwtAuthFilter extends OncePerRequestFilter {
    // Extracts token, validates it, and sets authentication context
}
```

---

## 🔧 Spring Security Configuration

```java
@Bean
SecurityFilterChain securityFilterChain(HttpSecurity http) throws Exception {
    return http
        .csrf(csrf -> csrf.disable())
        .authorizeHttpRequests(auth -> auth
            .requestMatchers("/auth/**", "/health", "/swagger-ui/**").permitAll()
            .anyRequest().authenticated()
        )
        .addFilterBefore(jwtAuthFilter, UsernamePasswordAuthenticationFilter.class)
        .build();
}
```

---

## 🧪 Testing Examples

### Login
```bash
curl -X POST http://localhost:8080/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"user","password":"pass"}'
```

### Access Protected API
```bash
curl http://localhost:8080/api/resource \
  -H "Authorization: Bearer <JWT>"
```

---

## 🚨 Common Errors

- **401 Unauthorized**: Missing, expired, or invalid token
- **Invalid signature**: Secret mismatch
- **Clock skew**: Server time incorrect

---

## 🧱 Best Practices

- Short-lived access tokens (10–20 minutes)
- Always use HTTPS
- Keep payload minimal
- Never log full tokens
- Rotate secrets periodically

---

## 🔁 Optional: Refresh Tokens

Use refresh tokens to obtain new access tokens without forcing users to re-login frequently.

---

**Document Status:** Saved and ready for reuse, sharing, or extension.

