﻿namespace eShop.UseCases.CustomerPortal.OrderConfirmationScreen;
public interface IViewOrderConfirmationUseCase
{
    Task<Order> ExecuteAsync(string uniqueId);
}
