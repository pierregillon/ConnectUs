﻿namespace ConnectUs.ClientSide
{
    public interface IRequestProcessor
    {
        Response Process(Request request);
    }
}