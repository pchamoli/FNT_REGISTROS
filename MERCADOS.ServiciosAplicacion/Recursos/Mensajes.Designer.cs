﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MERCADOS.ServiciosAplicacion.Recursos {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Mensajes {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Mensajes() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MERCADOS.ServiciosAplicacion.Recursos.Mensajes", typeof(Mensajes).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El índice de paginación debe ser mayor o igual a uno.
        /// </summary>
        internal static string error_IndexPageZero {
            get {
                return ResourceManager.GetString("error_IndexPageZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No se encuentra el objeto especificado en el repositorio de datos.
        /// </summary>
        internal static string error_NullObject {
            get {
                return ResourceManager.GetString("error_NullObject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El tamaño de página de la paginación debe ser mayor o igual a uno.
        /// </summary>
        internal static string error_PageSizeZero {
            get {
                return ResourceManager.GetString("error_PageSizeZero", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No se aceptan parámetros null en esta operación.
        /// </summary>
        internal static string error_ParameterNull {
            get {
                return ResourceManager.GetString("error_ParameterNull", resourceCulture);
            }
        }
    }
}
