import { ICuentaMunicipal } from "./ICuentaMunicipal";
import { IUsuario } from "./IUsuario";

export interface IUsuarioCM {
  usuario: IUsuario;
  cm: ICuentaMunicipal;
}
