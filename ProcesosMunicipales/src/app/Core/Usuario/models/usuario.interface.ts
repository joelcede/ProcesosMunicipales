enum BoolType {
  None = 2,
  True = 1,
  False = 0
}
export interface Usuario {
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
