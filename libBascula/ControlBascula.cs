using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libBascula
{
	public enum Puertos { COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9, COM10, COM11, COM12, COM13, COM14, COM15 }
	public enum EstadoConexion { Conectado, Desconectado }
	public enum Unidad { kg, lbs }

	public delegate void CambioValorEvenHandler ( object sender, CambioValorEventArgs e );
	public delegate void CambioEstadoEventHandler ( Object sender, EstadoConexion e );

	[ToolboxBitmap ( typeof ( resfinder ), "libBascula.scale.bmp" )]
	public partial class ControlBascula : Component, IBindableComponent
	{
		private CambioValorEventArgs _eventArgs;
		private EstadoConexion _estatus = EstadoConexion.Desconectado;
		private Puertos _Puerto = Puertos.COM3;
		private string _TextoRecibido = "";
		private bool _StatusLabelShowUnidad = true;
		private Unidad _Unidad = Unidad.kg;
		private PictureBox _StatusPicture = null;

		[Category ( "Bascula" )]
		[Description ( "Se produce cuando el valor recibido desde la Bascula cambia" )]
		public event CambioValorEvenHandler CambioValor;

		[Category ( "Bascula" )]
		[Description ( "Se produce cuando el estado de conexion de la Bascula cambia" )]
		public event CambioEstadoEventHandler CambioEstado;

		[Description ( "Nombre del puerto COM al que se conectara la bascula" )]
		[Category ( "Conexion Bascula" )]
		[DefaultValue ( typeof ( Puertos ), "COM3" )]
		public Puertos Puerto
		{
			get { return _Puerto; }
			set { if ( !SerialPort.IsOpen ) { SerialPort.PortName = value.ToString ( ); _Puerto = value; } }
		}

		[Description ( "Intervalo de envio de datos en milisegundos" )]
		[Category ( "Bascula" )]
		[DefaultValue ( 500 )]
		public int Intervalo { get; set; } = 500;


		[Description ( "Index del primer caracter tomado por el conversor de entrada de texto a double" )]
		[Category ( "Bascula" )]
		[DefaultValue ( 1 )]
		public int Inicio { get; set; } = 1;

		[Description ( "Numero de caracters tomados por el conversor de entrada de texto a double" )]
		[Category ( "Bascula" )]
		[DefaultValue ( 6 )]
		public int Fin { get; set; } = 6;

		[Description ( "Activa o desactiva el envio de caracteres al puerto de la bascula" )]
		[Category ( "Bascula" )]
		[DefaultValue ( false )]
		public bool ActivarEnvio { get; set; } = false;

		[Description ( "Caracter de final de linea, requerido para identificar el final del string recibido, formato Hexadecimal" )]
		[Category ( "Bascula" )]
		[DefaultValue ( "20" )]
		public string CaracterFinLinea { get; set; } = "20";

		[Browsable ( false )]
		public EstadoConexion Estatus { get { return _estatus; } }

		[Description ( "Texto a enviar al puerto COM,se envia solo si esta habilitado 'ActivarEnvio'" )]
		[Category ( "Bascula" )]
		[DefaultValue ( "" )]
		public string TextoAEnviar { get; set; }

		[Browsable ( false )]
		public string TextoRecibido
		{
			get { return _TextoRecibido; }
			private set
			{
				_TextoRecibido = value;
				_eventArgs.FullString = value;
			}
		}

		[Description ( "Tiempo de espera de lectura" )]
		[Category ( "Conexion Bascula" )]
		[DefaultValue ( 500 )]
		public int ReadTimeOut { get { return SerialPort.ReadTimeout; } set { SerialPort.ReadTimeout = value; } }

		[Description ( "Velocidad de transmision del puerto serie" )]
		[Category ( "Conexion Bascula" )]
		[DefaultValue ( 9600 )]
		public int BaudRate { get { return SerialPort.BaudRate; } set { if ( !SerialPort.IsOpen ) SerialPort.BaudRate = value; } }

		[Browsable ( false )]
		[DefaultValue ( 0D )]
		public double ValorBascula
		{
			get { return _eventArgs.NuevoValor; }
			set
			{
				_eventArgs.NuevoValor = value;
				_eventArgs.FormatedValue = String.Format ( "{0:00.00} {1}", value, StatusLabelShowUnidad ? Unidad.ToString ( ) : "" );
				this.OnCambioValor ( _eventArgs );
			}
		}

		[Description ( "Indica si el puerto Serie se conecta al iniciar el componente" )]
		[Category ( "Conexion Bascula" )]
		[DefaultValue ( false )]
		public bool ConectarAlIniciar { get; set; } = false;

		[Description ( "ToolStripStatusLabel donde se muestra el valor del peso leido de la bascula" )]
		[Category ( "Status Bascula" )]
		public ToolStripStatusLabel StatusLabel { get; set; }

		[Description ( "PictureBox donde se muestra el la imagen del estado la bascula" )]
		[Category ( "Status Bascula" )]
		public PictureBox StatusPicture { get { return _StatusPicture; } set { _StatusPicture = value; OnCambioEstado ( this.Estatus ); } }

		[Description ( "PictureBox donde se muestra el la imagen del estado la bascula" )]
		[Category ( "Status Bascula" )]
		public Image ConnectedImage { get; set; } = Properties.Resources.ScalesConnected_green;

		[Description ( "PictureBox donde se muestra el la imagen del estado la bascula" )]
		[Category ( "Status Bascula" )]
		public Image DisconnectedImage { get; set; } = Properties.Resources.ScalesConnected_red;


		[Description ( "Indica si se muestra o no la unidad de la bascula en el ToolStripStatusLabel" )]
		[Category ( "Bascula" )]
		[DefaultValue ( true )]
		[DisplayName ( "Mostrar Unidad" )]
		public bool StatusLabelShowUnidad { get { return _StatusLabelShowUnidad; } set { _StatusLabelShowUnidad = value; setStatusToolStripLabelText ( ValorBascula ); } }

		[Description ( "Unidad del valor de la bascula" )]
		[Category ( "Bascula" )]
		[DefaultValue ( typeof ( Unidad ), "kg" )]
		public Unidad Unidad { get { return _Unidad; } set { _Unidad = value; setStatusToolStripLabelText ( ValorBascula ); } }

		[Browsable ( false )]
		public SerialPort SerialPort { get; private set; }




		#region IBindableComponent Members
		private BindingContext bindingContext;
		private ControlBindingsCollection dataBindings;
		[Browsable ( false )]
		public BindingContext BindingContext
		{
			get
			{
				if ( bindingContext == null )
				{
					bindingContext = new BindingContext ( );
				}
				return bindingContext;
			}
			set
			{
				bindingContext = value;
			}
		}
		[DesignerSerializationVisibility ( DesignerSerializationVisibility.Content )]
		public ControlBindingsCollection DataBindings
		{
			get
			{
				if ( dataBindings == null )
				{
					dataBindings = new ControlBindingsCollection ( this );
				}
				return dataBindings;
			}
		}
		#endregion




		public ControlBascula ()
		{
			InitializeComponent ( );
			Iniciar ( );
		}

		public ControlBascula ( IContainer container )
		{
			container.Add ( this );
			InitializeComponent ( );
			Iniciar ( );
		}

		private void Iniciar ()
		{
			_eventArgs = new CambioValorEventArgs ( 0, "", "", this.Unidad );
			SerialPort = new SerialPort ( _Puerto.ToString ( ), 9600 );
			SerialPort.ReadTimeout = 500;

		}

		/// <summary>
		/// <para>Inicializa la bascula y la conecta si se configuro la propiedad ConectarAlIniciar como verdadero</para>
		/// <para>Agregue esté metodo al evento load del contenedor</para>  
		/// </summary>
		public void Initialize ()
		{
			if ( this.ConectarAlIniciar ) Conectar ( );

		}

		/// <summary>
		/// Intentara conectar la bascula con los nuevos valores para el puerto y el baudrate
		/// </summary>
		/// <param name="Puerto">Puerto Serie al que se intentara conectar</param>
		/// <param name="BaudRate">Velocidad de transmision del puerto serie</param>
		public void Conectar ( Puertos Puerto, int BaudRate )
		{
			this.Puerto = Puerto;
			this.BaudRate = BaudRate;
			this.Conectar ( );
		}
		/// <summary>
		/// Conecta la bascula con los valores determinados en las propiedades
		/// </summary>

		public void Conectar ()
		{
			if ( SerialPort.IsOpen )
			{
				this._estatus = EstadoConexion.Conectado;
				OnCambioEstado ( _estatus );
				throw new Exception ( "El puerto ya se encuentra abierto." );
			}

			SerialPort.PortName = this.Puerto.ToString ( );
			SerialPort.Open ( );
			SerialPort.NewLine = ( (char)Int16.Parse ( CaracterFinLinea, System.Globalization.NumberStyles.AllowHexSpecifier ) ).ToString ( );
			this._estatus = EstadoConexion.Conectado;
			OnCambioEstado ( _estatus );

			Task.Run ( () =>
			{
				while ( _estatus == EstadoConexion.Conectado && SerialPort.IsOpen )
				{
					try
					{

						if ( !SerialPort.IsOpen ) throw new Exception ( "El puerto no se encuentra abierto" );
						if ( ActivarEnvio ) SerialPort.Write ( TextoAEnviar );
						System.Threading.Thread.Sleep ( Intervalo >= 15 ? Intervalo - 10 : Intervalo );

					}
					catch ( Exception ex )
					{
						Console.WriteLine ( "Error al escribir en bascula:" + ex.Message, "Bascula" );
					}
				}
			} );

			Task.Run ( () =>
			{
				while ( _estatus == EstadoConexion.Conectado && SerialPort.IsOpen )
				{
					double val = 0.0;
					try
					{
						TextoRecibido = SerialPort.ReadLine ( );
						double.TryParse ( System.Text.RegularExpressions.Regex.Replace ( TextoRecibido, @"[^\d|.]", "" ), out val );
					}
					catch ( Exception )
					{
						val = 0.00;
					}
					finally
					{
						this.ValorBascula = val;
					}
				}
				this._estatus = EstadoConexion.Desconectado;
				OnCambioEstado ( _estatus );
			} );
		}

		/// <summary>
		/// Desconecta el puerto COM
		/// </summary>
		public void Desconectar ()
		{
			if ( SerialPort.IsOpen )
			{
				TextoRecibido = "";
				this.ValorBascula = 0;
				this.OnCambioValor ( _eventArgs );
				SerialPort.Close ( );
				this._estatus = EstadoConexion.Desconectado;
				OnCambioEstado ( _estatus );
			}
		}

		/// <summary>
		/// Intercambia el estado de la conexion del puerto
		/// </summary>
		public void toogleConection ()
		{
			if ( this.Estatus == EstadoConexion.Conectado )
			{
				this.Desconectar ( );
			}
			else
			{
				this.Conectar ( );
			}
		}


		protected virtual void OnCambioValor ( CambioValorEventArgs e )
		{
			e.Unidad = this.Unidad;
			if ( StatusLabel != null ) setStatusToolStripLabelText ( e.NuevoValor );
			CambioValor?.Invoke ( this, e );
		}
		protected virtual void OnCambioEstado ( EstadoConexion e )
		{
			setPictureBox ( e );
			CambioEstado?.Invoke ( this, e );
		}

		/// <summary>
		/// Delegado para el cambio de valor del StatusToolStripLabel
		/// </summary>
		/// <param name="value"></param>
		private void setStatusToolStripLabelText ( double value )
		{
			if ( this.StatusLabel != null )
			{
				if ( this.StatusLabel.GetCurrentParent ( ) != null && this.StatusLabel.GetCurrentParent ( ).InvokeRequired )
				{
					this.StatusLabel.GetCurrentParent ( ).Invoke ( new Action<double> ( setStatusToolStripLabelText ), value );
					return;
				};
				StatusLabel.Text = String.Format ( "{0:00.00} {1}", value, StatusLabelShowUnidad ? Unidad.ToString ( ) : "" );
			}
		}

		private void setPictureBox ( EstadoConexion e )
		{
			if ( StatusPicture != null )
			{
				if ( StatusPicture.InvokeRequired )
				{
					StatusPicture.Invoke ( new Action<EstadoConexion> ( setPictureBox ), e );
					return;
				}

				switch ( e )
				{
					case EstadoConexion.Conectado:
						StatusPicture.Image = this.ConnectedImage;
						break;
					case EstadoConexion.Desconectado:
						StatusPicture.Image = this.DisconnectedImage;
						break;
					default:
						StatusPicture.Image = null;
						break;
				}

			}
		}
	}



	/// <summary>
	/// Contenido del evento Cambio de Valor
	/// </summary>
	public class CambioValorEventArgs : EventArgs
	{
		/// <summary>
		/// Valor devuelto por el evento
		/// </summary>
		public double NuevoValor { get; set; }
		public string FullString { get; set; }
		public string FormatedValue { get; set; }
		public Unidad Unidad { get; set; }
		public CambioValorEventArgs ( double valor, string FullString, string formatedValue, Unidad Unidad )
		{
			this.NuevoValor = valor;
			this.FullString = FullString;
			this.Unidad = Unidad;
			this.FormatedValue = formatedValue;
		}
	}
}
