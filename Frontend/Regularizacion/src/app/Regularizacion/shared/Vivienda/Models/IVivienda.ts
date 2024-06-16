export interface IVivienda {
  id: string; // Correspondiente a Guid en C#
  direccion: string;
  codigoCatastral: string;
  telefono: string;
  coordenadas: string;
  //imagen: Uint8Array; // Correspondiente a byte[] en C#
  imagen: string;
  fechaCreacion: Date;
  fechaActualizacion: Date;
}
