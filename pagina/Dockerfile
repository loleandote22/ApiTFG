# Usa la imagen oficial de nginx ligera
FROM nginx:alpine

# Borra la configuración por defecto de nginx
RUN rm /etc/nginx/conf.d/default.conf

# Copia tu propia configuración de nginx
COPY pagina/nginx.conf /etc/nginx/conf.d/

# Copia los archivos estáticos al directorio que nginx sirve
COPY pagina/wwwroot /usr/share/nginx/html/

# Expone el puerto 80
EXPOSE 5001

# Comando por defecto
CMD ["nginx", "-g", "daemon off;"]
