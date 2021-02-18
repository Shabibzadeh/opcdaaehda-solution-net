#region Copyright (c) 2011-2021 Technosoftware GmbH. All rights reserved
//-----------------------------------------------------------------------------
// Copyright (c) 2011-2021 Technosoftware GmbH. All rights reserved
// Web: https://www.technosoftware.com 
// 
// The source code in this file is covered under a dual-license scenario:
//   - Owner of a purchased license: SCLA 1.0
//   - GPL V3: everybody else
//
// SCLA license terms accompanied with this source code.
// See SCLA 1.0://technosoftware.com/license/Source_Code_License_Agreement.pdf
//
// GNU General Public License as published by the Free Software Foundation;
// version 3 of the License are accompanied with this source code.
// See https://technosoftware.com/license/GPLv3License.txt
//
// This source code is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE.
//-----------------------------------------------------------------------------
#endregion Copyright (c) 2011-2021 Technosoftware GmbH. All rights reserved

#region Using Directives
using System;
#endregion

namespace Technosoftware.DaAeHdaClient.Ae
{
	/// <summary>
	/// The types of events that could be generated by a server.
	/// </summary>
	[Flags]
	public enum TsCAeEventType
	{
		/// <summary>
		/// Events that are not tracking or condition events.
		/// </summary>
		Simple = 0x0001,

		/// <summary>
		/// Events that represent occurrences which involve the interaction of the client with a target within the server.
		/// </summary>
		Tracking = 0x0002,

		/// <summary>
		/// Events that are associated with transitions in and out states defined by the server.
		/// </summary>
		Condition = 0x0004,

		/// <summary>
		/// All events generated by the server.
		/// </summary>
		All = 0xFFFF
	}
}
