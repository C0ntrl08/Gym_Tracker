﻿global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.ComponentModel.DataAnnotations;

global using Microsoft.EntityFrameworkCore;
global using Pomelo.EntityFrameworkCore.MySql;

// JWT
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;

// Controllers
global using Microsoft.AspNetCore.Mvc;
global using GymTrackerApi.Models;
global using Microsoft.AspNetCore.Identity;
global using GymTrackerApi.DataTransferObjects;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using Microsoft.AspNetCore.Authorization;
global using GymTrackerApi.Services;

global using GymTrackerApi.DataContexts;


global using System.Collections.Generic;