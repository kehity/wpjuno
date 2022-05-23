# wpjuno
La solución se compone de dos proyectos en .NET (proyecto acceso a datos - API) y una base de datos SQLServer,
para ejecutar la API ejecutar el proyecto wpjuno, para:
* determinar si una secuencia de ADN es mutante, hacer uso del servicio api/dna/mutant 
Ingresar una arreglo de strings como el siguiente ejemplo:
["ATGCGA","CAGTGC","TTATGT","AGAAGG","CCCCTA","TCACTG"]
* ver estadísticas de las verificaciones de ADN, hacer uso del servicio api/dna/stats
el algoritmo que evalua la cadena adn se encuentra en el programa: clsCadenaAdn.cs
