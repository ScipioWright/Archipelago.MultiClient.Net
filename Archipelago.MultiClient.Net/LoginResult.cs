﻿using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Packets;
using System.Collections.Generic;
using System.Linq;

namespace Archipelago.MultiClient.Net
{
    public abstract class LoginResult
    {
        public abstract bool Successful { get; }
    }

    public class LoginSuccessful : LoginResult
    {
        public override bool Successful => true;

        public int Team { get; }
        public int Slot { get; }

        /// <summary>
        /// Obsolete, use Location Helper instead
        /// </summary>
        public int[] MissingChecks { get; }
        /// <summary>
        /// Obsolete, use Location Helper instead
        /// </summary>
        public int[] LocationsChecked { get; }

        public Dictionary<string, object> SlotData { get; }

        public LoginSuccessful(ConnectedPacket connectedPacket)
        {
            Team = connectedPacket.Team;
            Slot = connectedPacket.Slot;
            MissingChecks = connectedPacket.MissingChecks.ToArray();
            LocationsChecked = connectedPacket.LocationsChecked.ToArray();
            SlotData = connectedPacket.SlotData;
        }
    }

    public class LoginFailure : LoginResult
    {
        public override bool Successful => false;

        public ConnectionRefusedError[] ErrorCodes { get; }
        public string[] Errors { get; }

        public LoginFailure(ConnectionRefusedPacket connectionRefusedPacket)
        {
            ErrorCodes = connectionRefusedPacket.Errors.ToArray();
            Errors = ErrorCodes.Select(GetErrorMessage).ToArray();
        }

        public LoginFailure(string message)
        {
            ErrorCodes = new ConnectionRefusedError[0];
            Errors = new[] { message };
        }

        static string GetErrorMessage(ConnectionRefusedError errorCode)
        {
            switch (errorCode)
            {
                case ConnectionRefusedError.InvalidSlot:
                    return "The slot name did not match any slot on the server.";
                case ConnectionRefusedError.InvalidGame:
                    return "The slot is set to a different game on the server.";
                case ConnectionRefusedError.SlotAlreadyTaken:
                    return "The slot already has a connection with a different uuid established.";
                case ConnectionRefusedError.IncompatibleVersion:
                    return "The client and server version mismatch.";
                case ConnectionRefusedError.InvalidPassword:
                    return "The password is invalid.";
                default:
                    return $"Unknown error: {errorCode}.";
            }
        }
    }
}