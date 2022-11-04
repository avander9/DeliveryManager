Esta herramienta nació por la necesidad de distribuir albaranes entre diferentes repartidores de una empresa de transporte.

## Nececidad

Para organizar el trabajo de los repartidores es necesario extraer de un PDF los albaranes(Sobre unos 800 cada día) que le corresponden a cada repartidor, cada albarán es una pagina del PDF y no necesariamente están en orden correlativo. La relación Albarán/Repartidor está en una hoja excel.

Este proceso se hacía de manera manual y requería unas 3 horas diarias.

## Solución

A partir del Excel y del PDF, el proceso crea un directorio por repartidor y busca en el PDF la página que corresponde al número de albarán, crea un nuevo PDF a partir de ese albaran y lo guarda en el directorio que le corresponde. En caso de que el albarán no se encuentre, se guarda el registro en una lista de errores que se exportará en formato excel al final del proceso.

Esta solución quedó obsoleta al cabo de 6 meses cuando se cambió el proceso de gestión de albaranes.
