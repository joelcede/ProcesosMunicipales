import { ICuentaMunicipal } from "../../Usuario/Models/ICuentaMunicipal";

export interface IViviendaFamProp {
  idVivienda: string; // Utilizamos string para representar Guid en TypeScript
  idUsuario: string;
  telefonoCelular: string;
  nombres: string;
  apellidos: string;
  dni: string;
  esPropietario: boolean;
  propietarioPrincipal: boolean;
  cuentaMunicipal: ICuentaMunicipal;
}
