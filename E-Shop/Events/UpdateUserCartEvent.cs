﻿using E_Shop.Core.Models;
using Prism.Events;

namespace E_Shop.Events
{
    public class UpdateUserCartEvent : PubSubEvent<CartItemEventModel>
    {
    }
}
