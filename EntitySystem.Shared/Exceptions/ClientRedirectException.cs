using System;
using EntitySystem.Shared.Models;

namespace EntitySystem.Shared.Exceptions;

    public class ClientRedirectException : Exception, IClientRedirectException
    {
        public string Uri { get; }

        public bool ClientSide { get; }

        public ClientRedirectException(ClientRedirectModel clientRedirectModel) : this(clientRedirectModel.Message, clientRedirectModel.Uri, clientRedirectModel.ClientSide)
        {
        }

        public ClientRedirectException(string reason, string uri, bool clientSide = false) : base(reason)
        {
            Uri = uri;

            ClientSide = clientSide;
        }
    }
