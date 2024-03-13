-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 13-03-2024 a las 02:15:25
-- Versión del servidor: 10.4.27-MariaDB
-- Versión de PHP: 8.2.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `farmacia`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `clientes`
--

CREATE TABLE `clientes` (
  `id` int(10) NOT NULL,
  `Nombre` varchar(30) NOT NULL,
  `Apellidos` varchar(30) NOT NULL,
  `Telefono` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `clientes`
--

INSERT INTO `clientes` (`id`, `Nombre`, `Apellidos`, `Telefono`) VALUES
(1, 'magaly', 'nicolas', 2147483647),
(2, 'Abigail', 'coronel', 1531639162),
(6, 'esme', 'bautista', 1234567899);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `factura`
--

CREATE TABLE `factura` (
  `Id` int(10) NOT NULL,
  `Fecha` date NOT NULL,
  `Id_Cliente` int(10) NOT NULL,
  `Total` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `factura`
--

INSERT INTO `factura` (`Id`, `Fecha`, `Id_Cliente`, `Total`) VALUES
(1, '2024-03-06', 0, 55),
(2, '2024-03-06', 0, 20),
(3, '2024-03-06', 1, 75),
(4, '2024-03-07', 1, 8),
(5, '2024-03-07', 1, 24),
(6, '2024-03-07', 1, 8),
(7, '2024-03-07', 1, 8),
(8, '2024-03-07', 1, 8),
(9, '2024-03-07', 1, 10),
(10, '2024-03-07', 1, 128),
(11, '2024-03-07', 1, 20),
(12, '2024-03-07', 1, 70),
(13, '2024-03-07', 1, 20),
(14, '2024-03-07', 1, 20),
(15, '2024-03-10', 6, 30),
(16, '2024-03-10', 6, 84),
(17, '2024-03-10', 2, 40),
(18, '2024-03-10', 2, 150),
(19, '2024-03-10', 1, 20),
(20, '2024-03-10', 1, 40),
(21, '2024-03-10', 1, 20),
(22, '2024-03-10', 1, 30),
(23, '2024-03-10', 1, 10),
(24, '2024-03-11', 1, 8),
(25, '2024-03-11', 1, 30),
(26, '2024-03-11', 1, 12),
(27, '2024-03-11', 1, 40),
(28, '2024-03-11', 1, 30),
(29, '2024-03-11', 1, 10),
(30, '2024-03-11', 2, 60);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `factura1`
--

CREATE TABLE `factura1` (
  `id` int(11) NOT NULL,
  `Id_Factura` int(11) NOT NULL,
  `Id_Producto` int(11) NOT NULL,
  `Cantidad` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `factura1`
--

INSERT INTO `factura1` (`id`, `Id_Factura`, `Id_Producto`, `Cantidad`) VALUES
(1, 1, 0, 10),
(2, 2, 0, 4),
(3, 3, 1, 10),
(4, 4, 1, 2),
(5, 5, 1, 2),
(6, 6, 1, 2),
(7, 7, 1, 2),
(8, 8, 1, 2),
(9, 9, 1, 2),
(10, 10, 1, 2),
(11, 11, 1, 2),
(12, 12, 1, 14),
(13, 13, 1, 2),
(14, 14, 1, 2),
(15, 15, 6, 1),
(16, 16, 6, 2),
(17, 17, 2, 1),
(18, 18, 2, 5),
(19, 19, 1, 2),
(20, 20, 1, 4),
(21, 21, 1, 2),
(22, 22, 1, 3),
(23, 23, 1, 1),
(24, 24, 1, 2),
(25, 25, 1, 3),
(26, 26, 1, 3),
(27, 27, 1, 4),
(28, 28, 1, 3),
(29, 29, 1, 1),
(30, 30, 2, 6);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productos`
--

CREATE TABLE `productos` (
  `id` int(10) NOT NULL,
  `Codigo_Barra` int(20) NOT NULL,
  `Producto` varchar(30) NOT NULL,
  `Cantidad_Existencia` int(10) NOT NULL,
  `Precio` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `productos`
--

INSERT INTO `productos` (`id`, `Codigo_Barra`, `Producto`, `Cantidad_Existencia`, `Precio`) VALUES
(1, 2147483647, 'gasas', 15, 10),
(2, 2147483647, 'aspirina', 80, 5),
(3, 2147483647, 'naproxeno', 10, 4),
(5, 2147483647, 'agua', 14, 10),
(6, 2147483647, 'suero', 40, 12),
(8, 2147483647, 'jabon', 18, 12),
(18, 2147483647, 'pomada de la campana', 5, 40);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `Id` int(10) NOT NULL,
  `Usuario` varchar(30) NOT NULL,
  `Contraseña` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`Id`, `Usuario`, `Contraseña`) VALUES
(1, 'Majo', 1234),
(2, 'Judith', 5678),
(3, 'magaly', 123),
(4, 'abi', 12);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `clientes`
--
ALTER TABLE `clientes`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `factura`
--
ALTER TABLE `factura`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `factura1`
--
ALTER TABLE `factura1`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `productos`
--
ALTER TABLE `productos`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `clientes`
--
ALTER TABLE `clientes`
  MODIFY `id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `factura`
--
ALTER TABLE `factura`
  MODIFY `Id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT de la tabla `factura1`
--
ALTER TABLE `factura1`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `Id` int(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
