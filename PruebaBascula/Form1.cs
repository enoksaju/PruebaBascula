using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaBascula
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ( );
			ctlBascula.Initialize ( );
		}


		private void button1_Click ( object sender, EventArgs e )
		{
			this.ctlBascula.toogleConection ( );
		}

		private async void ctlBascula_CambioValor ( object sender, libBascula.CambioValorEventArgs e )
		{
			await Task.Run ( () =>
			{
				try
				{
					setFullString ( e.FullString );
					setFormatedString ( e.FormatedValue);
					setValue ( e.NuevoValor );
				}
				catch ( Exception EX )
				{
					Console.WriteLine ( EX );
				}
			} );
		}

		private void Form1_Load ( object sender, EventArgs e )
		{

		}


		private void setFullString ( string Value )
		{
			if ( label1.InvokeRequired )
			{
				label1.Invoke ( new Action<string> ( setFullString ), Value );
				return;
			}
			label1.Text = Value.ToString ( );
		}


		private void setFormatedString ( string Value )
		{
			if ( label2.InvokeRequired )
			{
				label2.Invoke ( new Action<string> ( setFormatedString ), Value );
				return;
			}
			label2.Text = Value.ToString ( );
		}

		private void setValue ( double Value )
		{
			if ( label3.InvokeRequired )
			{
				label3.Invoke ( new Action<double> ( setValue ), Value );
				return;
			}
			label3.Text = Value.ToString ( );
		}

		private void ctlBascula_CambioEstado ( object sender, libBascula.EstadoConexion e )
		{
			button1.Text = e == libBascula.EstadoConexion.Conectado ? "Desconectar" : "Conectar";
		}
	}
}
