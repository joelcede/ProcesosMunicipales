export interface IViviendaUsuario {
  idVivienda: string; // Correspondiente a Guid en C#
  idUsuario: string; // Correspondiente a Guid en C#
  telefonoCelular: string;
  nombres: string;
  apellidos: string;
  dni: string;
  esPropietario: boolean;
  propietarioPrincipal: boolean;
}
