export interface IUsuario {
  id: string;
  nombres: string;
  apellidos: string;
  dni: string;
  telefonoCelular: string;
  telefonoConvencional: string;
  esPrincipal: boolean;
  fechaCreacion: Date;
  fechaModificacion: Date;
}
