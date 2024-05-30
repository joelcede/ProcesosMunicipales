export interface IVivienda {
  id: string; // Correspondiente a Guid en C#
  direccion: string;
  codigoCatastral: string;
  telefono: string;
  coordenadas: string;
  //imagen: Uint8Array; // Correspondiente a byte[] en C#
  imagen: string;
  //ciudad: CiudadType;
  //provincia: ProvinciaType;
  //pais: PaisType;

  fechaCreacion: Date;
  fechaActualizacion: Date;
}

// Asumiendo que tienes estas enumeraciones definidas en TypeScript:
export enum CiudadType {
  Guayaquil = 'Guayaquil',
  Quito = 'Quito',
  Cuenca = 'Cuenca'
  // Agrega más ciudades según sea necesario
}

export enum ProvinciaType {
  Guayas = 'Guayas',
  Pichincha = 'Pichincha',
  Azuay = 'Azuay'
  // Agrega más provincias según sea necesario
}

export enum PaisType {
  Ecuador = 'Ecuador',
  Colombia = 'Colombia',
  Peru = 'Peru'
  // Agrega más países según sea necesario
}
