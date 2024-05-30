export interface CuentaMunicipal {
  id: string; // Correspondiente a Guid en C#
  idUsuario: string; // Correspondiente a Guid en C#
  correoElectronico: string;
  password: string;
  cuentaMunicipal: string;
  contrasenaMunicipal: string;
  esPropietario: boolean;
  fechaCreacion: Date;
  fechaModificacion: Date;
}
