// ------------------------------------------------------------------
// DirectX.Capture
//
// History:
//	2003-Jan-24		BL		- created
//
// Copyright (c) 2003 Brian Low
// ------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using DShowNET;

namespace DirectX.Capture
{
	/// <summary>
	///  Specify the frequency of the TV tuner.
	/// </summary>
	public enum TunerInputType
	{
		/// <summary> Cable frequency </summary>
		Cable,
		/// <summary> Antenna frequency </summary>
		Antenna
	}


	/// <summary>
	///  Control and query a hardware TV Tuner.
	/// </summary>
	/// <remarks>
	///  See the <see cref="ChannelAvailable"/> for more information on 
	///  determining valid channels.
	/// </remarks>
	public class Tuner : IDisposable
	{
		// ---------------- Private Properties ---------------

		protected IAMTVTuner tvTuner = null;		



		// ------------------- Constructors ------------------

		/// <summary> Initialize this object with a DirectShow tuner </summary>
		public Tuner(IAMTVTuner tuner)
		{
			tvTuner = tuner;
		}



		// ---------------- Public Properties ---------------
		
		/// <summary>
		///  Get or set the TV Tuner channel.
		/// </summary>
		/// <remarks>
		/// Each time the channel is changed the tuner will search for the exact
		/// frequency the channel is broadcast on. See <see cref="ChannelAvailable"/> 
		/// for more information.
		/// </remarks>
		public int Channel
		{
			get
			{
				int channel;
				int v, a;
				int hr = tvTuner.get_Channel( out channel, out v, out a );
				if ( hr < 0 ) Marshal.ThrowExceptionForHR( hr );
				return( channel );
			}

			set
			{
				int hr = tvTuner.put_Channel( value, AMTunerSubChannel.Default, AMTunerSubChannel.Default );
				if ( hr < 0 ) Marshal.ThrowExceptionForHR( hr );
			}
		}

		/// <summary>
		///  Get or set the tuner frequency (cable or antenna).
		/// </summary>
		public TunerInputType InputType
		{
			get
			{
				DShowNET.TunerInputType t;
				int hr = tvTuner.get_InputType( 0, out t );
				if ( hr < 0 ) Marshal.ThrowExceptionForHR( hr );
				return( (TunerInputType) t );
			}
			set
			{
				DShowNET.TunerInputType t = (DShowNET.TunerInputType) value;
				int hr = tvTuner.put_InputType( 0, t );
				if ( hr < 0 ) Marshal.ThrowExceptionForHR( hr );
			}
		}

		/// <summary>
		///  Get or set the country code. Use the country code to set default frequency mappings.
		/// </summary>
		/// <remarks>
		/// Below is a sample of available country codes:
		/// <list type="bullet">
		///   <item>1 - US</item>
		/// </list>
		/// For a full list of country codes, see the DirectX 9.0 
		/// documentation topic "Country/Region Assignments"
		/// </remarks>
		public int CountryCode
		{
			get
			{
				int c;
				int hr = tvTuner.get_CountryCode( out c );
				if ( hr < 0 ) Marshal.ThrowExceptionForHR( hr );
				return( c );
			}
			set
			{
				int hr = tvTuner.put_CountryCode( value );
				if ( hr < 0 ) Marshal.ThrowExceptionForHR( hr );
			}
		}

		/// <summary>
		///  Lowest channel available in the tuning space.
		/// </summary>
		/// <remarks>
		///  <para>
		///  This property is read-only.</para>
		///  
		///  <para>
		///  This method provides the lowest channel that the tuner
		///  can physically tune to. Use AutoTune to determine if
		///  the channel has a valid TV signal. </para>
		/// </remarks>
		public int ChannelMin
		{
			get
			{
				int min, max;
				int hr = tvTuner.ChannelMinMax( out min, out max );
				if ( hr < 0 ) Marshal.ThrowExceptionForHR( hr );
				return min;
			}
		}

		/// <summary>
		///  Highest channel available in the tuning space.
		/// </summary>
		/// <remarks>
		///  <para>
		///  This property is read-only.</para>
		///  
		///  <para>
		///  This method provides the lowest channel that the tuner
		///  can physically tune to. Use AutoTune to determine if
		///  the channel has a valid TV signal. </para>
		/// </remarks>
		public int ChannelMax
		{
			get
			{
				int min, max;
				int hr = tvTuner.ChannelMinMax( out min, out max );
				if ( hr < 0 ) Marshal.ThrowExceptionForHR( hr );
				return max;
			}
		}


		/// <summary>
		///  Indicates whether a TV signal is present on the current channel (horizontal lock).
		/// </summary>
		/// <remarks>
		///  <para>
		///  SignalPresent provides a more rigorous test for a valid channel than ChannelAvailable. 
		///  As a result, SignalPresent may return false for some channels that are still
		///  viewable. </para>
		///  
		///  <para>
		///  For TV tuning there are 2 "stages" to locking on to a channel. The first
		///  stage is a frequency lock and the second is horizontal lock. A frequency lock occurs
		///  when the tuner has detected a correctly modulated data signal that may or may not be 
		///  a TV signal. A horizontal lock occurs when the decoder has detected a valid TV signal. </para>
		///
		///  <para>
		///  The SignalPresent method checks for a horizontal lock on the current channel. 
		///  The <see cref="ChannelAvailable"/> method checks for only a frequency lock.
		///  </para>
		///  
		///  <para>
		///  If the signal strength cannot be determined, a NotSupportedException
		///  is thrown.</para>
		/// </remarks>
		public bool SignalPresent
		{
			get
			{
				AMTunerSignalStrength sig;
				int hr = tvTuner.SignalPresent( out sig );
				if ( hr < 0 ) Marshal.ThrowExceptionForHR( hr );
				if ( sig == AMTunerSignalStrength.NA ) throw new NotSupportedException("Signal strength not available.");
				return( sig == AMTunerSignalStrength.SignalPresent );
			}
		}

		/// <summary>
		///  Determines if the tuner can tune to a particular channel.
		/// </summary>
		/// <remarks>
		///  <para>
		///  An automated scan to find available channels:
		///  <list type="number">
		///   <item>Use <see cref="ChannelMin"/> and <see cref="ChannelMax"/> to determine 
		///			the range of available channels.</item>
		///   <item>For each channel, call ChannelAvailable. If this method returns false, do not 
		///			display the channel to the user. If this method returns true, it 
		///			will have found the exact frequency for the channel.</item>
		///	  <item>If ChannelAvailable is finding too many channels with just noise then 
		///			check the <see cref="SignalPresent"/> property after calling ChannelAvailable. 
		///			If SignalPresent is true, then the channel is most likely a valid, viewable
		///			channel. However this risks missing viewable channels with moderate noise.
		///			See <see cref="SignalPresent"/> for more information on locking on to 
		///			a channel.</item>
		///  </list>
		///  </para>
		///  
		///  <para>
		///  It is no longer required to perform a scan for each chanel's exact 
		///  frequency. The tuner automatically finds the exact frequency each 
		///  time the channel is changed. </para>
		///  
		///  <para>
		///  This method correctly uses frequency-overrides. As described in
		///  the DirectX SDK topic "Collecting Fine-Tuning Information", this method
		///  does not use the IAMTVTuner.AutoTune() method. Instead it uses the
		///  suggested put_Channel() method. </para>
		/// </remarks>
		/// <param name="channel">TV channel number</param>
		/// <returns>True if the channel's frequence was found, false otherwise.</returns>
 		public bool ChannelAvailable( int channel )
		{
			int hr = tvTuner.put_Channel( channel, AMTunerSubChannel.Default, AMTunerSubChannel.Default );
			if ( hr < 0 ) Marshal.ThrowExceptionForHR( hr ); 
			return ( hr == 0);
		}

		/// <summary>
		///  The current video frequency in hertz (Hz)
		/// </summary>
		/// <remarks>
		///  This property is read only. Use <see cref="Channel"/> or
		///  <see cref="AutoTune"/> to change the channel.
		/// </remarks>
		public int VideoFrequency
		{
			get
			{
				int freq;
				int hr = tvTuner.get_VideoFrequency( out freq );
				if ( hr < 0 ) Marshal.ThrowExceptionForHR( hr ); 
				return ( freq );
			}
		}

		/// <summary>
		///  The current audio frequency in hertz (Hz)
		/// </summary>
		/// <remarks>
		///  This property is read only. Use <see cref="Channel"/> or
		///  <see cref="AutoTune"/> to change the channel.
		/// </remarks>
		public int AudioFrequency
		{
			get
			{
				int freq;
				int hr = tvTuner.get_AudioFrequency( out freq );
				if ( hr < 0 ) Marshal.ThrowExceptionForHR( hr ); 
				return ( freq );
			}
		}




		public enum AMTunerModeType
		{
			Default		= 0x0000,	// AMTUNER_MODE_DEFAULT : default tuner mode
			TV			= 0x0001,	// AMTUNER_MODE_TV : tv
			FMRadio		= 0x0002,	// AMTUNER_MODE_FM_RADIO : fm radio
			AMRadio		= 0x0004,	// AMTUNER_MODE_AM_RADIO : am radio
			Dss			= 0x0008	// AMTUNER_MODE_DSS : dss
		}

		public struct AvAudioModes
		{
			public bool Default, TV, FMRadio, AMRadio, Dss; 
			public AvAudioModes(bool Default, bool TV, bool FMRadio, bool AMRadio, bool Dss)
			{
				this.Default = Default;
				this.TV = TV;
				this.FMRadio = FMRadio;
				this.AMRadio = AMRadio;
				this.Dss = Dss;
			}
		}

		/// 
		/// Retrieves or sets the current mode on a multifunction tuner.
		/// 
		public AMTunerModeType AudioMode
		{
			get
			{
				DShowNET.AMTunerModeType AudioMode;
				int hr = tvTuner.get_Mode(out AudioMode);
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
				return( (AMTunerModeType) AudioMode );
			}
			set
			{
				DShowNET.AMTunerModeType AudioMode = (DShowNET.AMTunerModeType) value;
				int hr = tvTuner.put_Mode(AudioMode);
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
			}
		}


		/// 
		/// Retrieves the tuner's supported modes.
		/// 
		public AvAudioModes AvailableAudioModes
		{
			get
			{
				DShowNET.AMTunerModeType AudioMode;
				int hr = tvTuner.GetAvailableModes(out AudioMode);
				if ( hr != 0 ) Marshal.ThrowExceptionForHR( hr );
				AvAudioModes AvModes;

				if ((int)AudioMode == (int)AMTunerModeType.TV)
				{
					AvModes = new AvAudioModes(true,true,false,false,false);
				}
				else if ((int)AudioMode == (int)AMTunerModeType.TV + (int)AMTunerModeType.AMRadio)
				{
					AvModes = new AvAudioModes(true,true,false,true,false);
				}
				else if ((int)AudioMode == (int)AMTunerModeType.TV + (int)AMTunerModeType.FMRadio)
				{
					AvModes = new AvAudioModes(true,true,true,false,false);
				}
				else if ((int)AudioMode == (int)AMTunerModeType.TV + (int)AMTunerModeType.Dss)
				{
					AvModes = new AvAudioModes(true,true,false,false,true);
				}
				else if ((int)AudioMode == (int)AMTunerModeType.TV + (int)AMTunerModeType.AMRadio + (int)AMTunerModeType.FMRadio)
				{
					AvModes = new AvAudioModes(true,true,true,true,false);
				}
				else if ((int)AudioMode == (int)AMTunerModeType.TV + (int)AMTunerModeType.AMRadio + (int)AMTunerModeType.Dss)
				{
					AvModes = new AvAudioModes(true,true,false,true,true);
				}
				else if ((int)AudioMode == (int)AMTunerModeType.TV + (int)AMTunerModeType.FMRadio + (int)AMTunerModeType.Dss)
				{
					AvModes = new AvAudioModes(true,true,true,false,true);
				}
				else if ((int)AudioMode == (int)AMTunerModeType.TV + (int)AMTunerModeType.AMRadio + (int)AMTunerModeType.FMRadio + (int)AMTunerModeType.Dss)
				{
					AvModes = new AvAudioModes(true,true,true,true,true);
				}
				else
				{
					AvModes = new AvAudioModes(false,false,false,false,false);
				}

				return( AvModes );
			}
		}

		// ---------------- Public Methods ---------------

		public void Dispose()
		{
			if ( tvTuner != null )
				Marshal.ReleaseComObject( tvTuner ); tvTuner = null;
		}
	}
}
