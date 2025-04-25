# Exercici 1

La librería System.Diagnostics sirve para leer y escribir en registros de eventos, tambiés hacer diagnósticos de rendimiento, rastrear ejecuciones y depurar aplicaciones. Es muy útil para desarrollar, hacer pruebas y monitorear aplicaciones.

- Process.Start(): Inicia un nuevo proceso.
- Process.Kill(): Finaliza el proceso de manera forzada.
- Process.GetCurrentProcess(): Devuelve el proceso actual.

# Exercici 3

si abro más de una pestaña no se abren más hilos, ya que el sistema operativo solamente detecta el navegado como 1 solo proceso, por eso es que los navegadores estan implementando la funcionalidad de prioridades en la pestañas por su cuenta, ya que el sistema operativo no capaz de hacerlo y eso conlleva que gasten más memoria de la que deberían?

## Comparación entre Threads y Task:

La clase Thread pertenece al espacio de nombres System.Threading y permite crear y gestionar hilos manualmente. Es útil para ejecutar código en paralelo, pero requiere más control por parte del programador.



| __Característica__ | __Thread__ | __Task (System.Threading.Tasks)__ |
|---------------|----------------|-----------------|
| **Nivel de abstracción**| Bajo (control manual del hilo)| Alto (gestión automática)|
| **Forma de creación** | new Thread(método).Start()| Task.Run()|
| **Paralelismo**| Sí| Sí|
| **Escalabilidad**| Limitada (cada hilo es del SO)| Alta (usa el ThreadPool)|
| **Retorno de resultados** | No| Sí (Task<T>)|
| **Espera hasta finalizar** | Join()| await, .Wait(), .Result |
| **Cancelación**| Difícil de implementar| Sí (CancellationToken)|
| **Control de excepciones** | Manual | Manejable con try/catch y await|
| **Gestión de prioridades**| Sí (con Priority) | No directa|
| **Uso recomendado** | Escenarios de bajo nivel o legacy| Desarrollo moderno, código asincróno|

