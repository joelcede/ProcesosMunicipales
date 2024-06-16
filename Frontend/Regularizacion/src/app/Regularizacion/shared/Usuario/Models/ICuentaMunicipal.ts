export interface ICuentaMunicipal {
  id: string; // Correspondiente a Guid en C#
  idUsuario: string; // Correspondiente a Guid en C#
  cuentaMunicipal: string;
  contrasenaMunicipal: string;
  esPropietario: boolean;
  fechaCreacion: Date;
  fechaModificacion: Date;
}
