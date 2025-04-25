## NETCrazyZombie multijugador

### Mejoras sugeridas:

- Crear el sistema de proyectiles de igual forma que se hace en NETTanks.
  - Prefab base
  - Prefab cliente
  - Prefab servidor
  Hecho.

- Separar el sistema de salud del jugador de forma similar a NETTanks.

- Separar el sistema del display de salud de forma similar a NETTanks.

- Implementar un sistema de respawn que funcione correctamente. Hecho

### Mejoras sugeridas (investigación, avanzado):

- Cambiar el sistema de cámaras, utilizando el paquete Cinemachine. Se utiliza en NETTanks en la rama online.

### Bugs detectados y corregidos

1. **Zombis Tropezando en Escaleras:**
   - Se corrigió un bug donde los zombies se movían de forma errática y no podían alcanzar al jugador si chocaban con las escaleras. Esto se solucionó ajustando la lógica de navegación de los enemigos para evitar los bloqueos(continous dinamyc).

2. **Movimiento Errático del Personaje en Esquinas:**
   - Se corrigió el error donde el personaje giraba de manera extraña al acercarse a una esquina, lo que afectaba el control y la experiencia de juego(con physic material).